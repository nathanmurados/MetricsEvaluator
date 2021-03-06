using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class ResultsPathPresenter : IResultsPathPresenter
    {
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public IChildDirectoryCountEvaluator ChildDirectoryCountEvaluator { get; private set; }
        public IEnableGroupingEvaluator EnableGroupingEvaluator { get; private set; }

        public ResultsPathPresenter(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator, IPathExistenceEvaluator pathExistenceEvaluator, IChildDirectoryCountEvaluator childDirectoryCountEvaluator, IEnableGroupingEvaluator enableGroupingEvaluator)
        {
            EnableGroupingEvaluator = enableGroupingEvaluator;
            ChildDirectoryCountEvaluator = childDirectoryCountEvaluator;
            PathExistenceEvaluator = pathExistenceEvaluator;
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.ResultsPath,
                Description = "Select results directory"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ResultsPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.ResultsDirectory = Properties.Settings.Default.ResultsPath;
                viewModel.IsIdle = EnableDiagnosticsEvaluator.Evaluate();
                viewModel.IsValidResultsDirectory = PathExistenceEvaluator.Evaluate(Properties.Settings.Default.ResultsPath);
                viewModel.EnableGroupSelecting = EnableGroupingEvaluator.Evaluate(viewModel);
            }
        }
    }
}