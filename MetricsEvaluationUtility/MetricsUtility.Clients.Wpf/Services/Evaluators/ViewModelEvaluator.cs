using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ViewModelEvaluator : IViewModelEvaluator
    {
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }
        public IChildDirectoryCountEvaluator ChildDirectoryCountEvaluator { get; private set; }
        public IFoldersPerGroupEvaluator FoldersPerGroupEvaluator { get; private set; }
        public IEnableGroupingEvaluator EnableGroupingEvaluator { get; private set; }

        public ViewModelEvaluator(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator, IChildDirectoryCountEvaluator childDirectoryCountEvaluator, IPathExistenceEvaluator pathExistenceEvaluator, IFoldersPerGroupEvaluator foldersPerGroupEvaluator, IEnableGroupingEvaluator enableGroupingEvaluator)
        {
            EnableGroupingEvaluator = enableGroupingEvaluator;
            FoldersPerGroupEvaluator = foldersPerGroupEvaluator;
            PathExistenceEvaluator = pathExistenceEvaluator;
            ChildDirectoryCountEvaluator = childDirectoryCountEvaluator;
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public ViewModel Evaluate()
        {
            var isValidResultsDirectory = PathExistenceEvaluator.Evaluate(Properties.Settings.Default.ResultsPath);
            var isIdle = EnableDiagnosticsEvaluator.Evaluate();

            return new ViewModel
            {
                SolutionToAnalyse = string.IsNullOrWhiteSpace(Properties.Settings.Default.InspectionPath) ? "(None)" : Properties.Settings.Default.InspectionPath,
                AllowFolderChanges = true,
                IsIdle = isIdle,
                ResultsDirectory = string.IsNullOrWhiteSpace(Properties.Settings.Default.ResultsPath) ? "(None)" : Properties.Settings.Default.ResultsPath,
                IsValidResultsDirectory = isValidResultsDirectory,
                IsValidInspectionDirectory = PathExistenceEvaluator.Evaluate(Properties.Settings.Default.InspectionPath),
                GroupCount = 1,
                ChildDirectoryCount = ChildDirectoryCountEvaluator.Evaluate(),
                FoldersPerGroup = FoldersPerGroupEvaluator.Evaluate(ChildDirectoryCountEvaluator.Evaluate(), 1),
                EnableGroupSelecting = EnableGroupingEvaluator.Evaluate(new ViewModel { IsValidResultsDirectory = isValidResultsDirectory, IsIdle = isIdle })
            };
        }
    }
}