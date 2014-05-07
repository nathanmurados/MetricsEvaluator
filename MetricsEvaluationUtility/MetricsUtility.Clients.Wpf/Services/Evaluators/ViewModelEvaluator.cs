using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ViewModelEvaluator : IViewModelEvaluator
    {
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public ViewModelEvaluator(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public ViewModel Evaluate()
        {
            return new ViewModel
            {
                SolutionToAnalyse = string.IsNullOrWhiteSpace(Properties.Settings.Default.InspectionPath) ? "(None)" : Properties.Settings.Default.InspectionPath,
                AllowInteractions = true,
                EnableDiagnostics = EnableDiagnosticsEvaluator.Evaluate(),
                ResultsDirectory = string.IsNullOrWhiteSpace(Properties.Settings.Default.ResultsPath) ? "(None)" : Properties.Settings.Default.ResultsPath
            };
        }
    }
}