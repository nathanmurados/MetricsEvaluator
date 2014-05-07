using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class SolutionPathPresenter : ISolutionPathPresenter
    {
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public SolutionPathPresenter(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.InspectionPath,
                Description = "Select folder to inspect"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.InspectionPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.SolutionToAnalyse = Properties.Settings.Default.InspectionPath;
                viewModel.EnableDiagnostics = EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}