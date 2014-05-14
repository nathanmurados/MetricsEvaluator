using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class GeneratedCssPathPresenter : IGeneratedCssPathPresenter
    {
        public IHasRefactorPathsEvaluator HasRefactorPathsEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public GeneratedCssPathPresenter(IHasRefactorPathsEvaluator hasRefactorPathsEvaluator, IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
            HasRefactorPathsEvaluator = hasRefactorPathsEvaluator;
        }


        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.GeneratedFilesPath,
                Description = "Select the folder you would to store  generated CSS files"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.GeneratedFilesPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.GeneratedCssDirectory = Properties.Settings.Default.GeneratedFilesPath;
                viewModel.HasCssRefactorPaths = HasRefactorPathsEvaluator.Evaluate();
                EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}