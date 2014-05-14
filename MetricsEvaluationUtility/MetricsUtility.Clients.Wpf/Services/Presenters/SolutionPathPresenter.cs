using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class SolutionPathPresenter : ISolutionPathPresenter
    {
        public IHasRefactorPathsEvaluator HasRefactorPathsEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public SolutionPathPresenter(IHasRefactorPathsEvaluator hasRefactorPathsEvaluator, IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
            HasRefactorPathsEvaluator = hasRefactorPathsEvaluator;
        }

        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.RefactorPath,
                Description = "Select the route directory of the solution you are refactoring"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.SolutionPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.SolutionDirectory = Properties.Settings.Default.SolutionPath;
                viewModel.HasCssRefactorPaths = HasRefactorPathsEvaluator.Evaluate();
                EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}