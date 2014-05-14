using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class RefactorPathPresenter : IRefactorPathPresenter
    {
        public IHasRefactorPathsEvaluator HasRefactorPathsEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public RefactorPathPresenter(IHasRefactorPathsEvaluator hasRefactorPathsEvaluator, IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
            HasRefactorPathsEvaluator = hasRefactorPathsEvaluator;
        }


        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.RefactorPath,
                Description = "Select the target CSS folder to refactor"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.RefactorPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.RefactorCssDirectory = Properties.Settings.Default.RefactorPath;
                viewModel.HasCssRefactorPaths = HasRefactorPathsEvaluator.Evaluate();
                viewModel.IsIdle = EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}