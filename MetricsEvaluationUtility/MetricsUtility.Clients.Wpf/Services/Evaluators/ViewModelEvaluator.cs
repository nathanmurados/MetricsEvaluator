using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ViewModelEvaluator : IViewModelEvaluator
    {
        public ViewModel Evaluate()
        {
            return new ViewModel
            {
                SolutionToAnalyse = string.IsNullOrWhiteSpace(Properties.Settings.Default.InspectionPath) ? "(None)" : Properties.Settings.Default.InspectionPath,
                AllowInteractions = true,
                ResultsDirectory = string.IsNullOrWhiteSpace(Properties.Settings.Default.ResultsPath) ? "(None)" : Properties.Settings.Default.InspectionPath,
            };
        }
    }
}