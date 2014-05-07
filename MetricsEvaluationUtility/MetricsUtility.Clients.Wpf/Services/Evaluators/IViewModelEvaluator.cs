using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public interface IViewModelEvaluator
    {
        ViewModel Evaluate();
    }
}