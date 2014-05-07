using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ViewModelEvaluator : IViewModelEvaluator
    {
        public ViewModel Evaluate()
        {
            return new ViewModel
            {
                SolutionToAnalyse = string.IsNullOrWhiteSpace(Properties.Settings.Default.SolutionToAnalyse) ? "(None)" : Properties.Settings.Default.SolutionToAnalyse
            };
        }
    }
}