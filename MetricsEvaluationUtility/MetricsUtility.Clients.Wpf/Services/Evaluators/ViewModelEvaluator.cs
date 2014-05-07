using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ViewModelEvaluator : IViewModelEvaluator
    {
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }
        public IFolderExistenceEvaluator FolderExistenceEvaluator { get; private set; }

        public ViewModelEvaluator(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator, IFolderExistenceEvaluator folderExistenceEvaluator)
        {
            FolderExistenceEvaluator = folderExistenceEvaluator;
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public ViewModel Evaluate()
        {
            return new ViewModel
            {
                SolutionToAnalyse = string.IsNullOrWhiteSpace(Properties.Settings.Default.InspectionPath) ? "(None)" : Properties.Settings.Default.InspectionPath,
                AllowInteractions = true,
                EnableDiagnostics = EnableDiagnosticsEvaluator.Evaluate(),
                ResultsDirectory = string.IsNullOrWhiteSpace(Properties.Settings.Default.ResultsPath) ? "(None)" : Properties.Settings.Default.ResultsPath,
                IsValidResultsDirectory = FolderExistenceEvaluator.Evaluate(Properties.Settings.Default.ResultsPath),
                IsValidInspectionDirectory = FolderExistenceEvaluator.Evaluate(Properties.Settings.Default.InspectionPath)
            };
        }
    }
}