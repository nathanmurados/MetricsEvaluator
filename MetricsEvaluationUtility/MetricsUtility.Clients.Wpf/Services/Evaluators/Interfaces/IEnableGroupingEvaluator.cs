using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IEnableGroupingEvaluator
    {
        bool Evaluate(ViewModel viewModel);
    }
}