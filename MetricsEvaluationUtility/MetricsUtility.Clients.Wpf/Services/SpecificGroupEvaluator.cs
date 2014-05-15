using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class SpecificGroupEvaluator : ISpecificGroupEvaluator
    {
        public int Evaluate(ViewModel viewModel)
        {
            return viewModel.EnableSpecificGroup == true ? viewModel.SpecificGroupToInspect : 0;
        }
    }
}