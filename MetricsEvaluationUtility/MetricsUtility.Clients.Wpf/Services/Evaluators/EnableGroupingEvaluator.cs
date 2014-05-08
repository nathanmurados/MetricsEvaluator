using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class EnableGroupingEvaluator : IEnableGroupingEvaluator
    {
        public bool Evaluate(ViewModel viewModel)
        {
            return viewModel.IsValidResultsDirectory && viewModel.IsIdle;
        }
    }
}