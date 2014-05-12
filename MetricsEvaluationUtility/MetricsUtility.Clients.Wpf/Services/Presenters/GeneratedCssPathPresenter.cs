using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class GeneratedCssPathPresenter : IGeneratedCssPathPresenter
    {
        public IHasCssRefactorPathsEvaluator HasCssRefactorPathsEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public GeneratedCssPathPresenter(IHasCssRefactorPathsEvaluator hasCssRefactorPathsEvaluator, IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
            HasCssRefactorPathsEvaluator = hasCssRefactorPathsEvaluator;
        }


        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.GeneratedCssPath,
                Description = "Select the folder you would to store  generated CSS files"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.GeneratedCssPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.GeneratedCssDirectory = Properties.Settings.Default.GeneratedCssPath;
                viewModel.HasCssRefactorPaths = HasCssRefactorPathsEvaluator.Evaluate();
                EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}