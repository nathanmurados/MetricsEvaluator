using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface ISpecificGroupEvaluator
    {
        int Evaluate(ViewModel viewModel);
    }
}