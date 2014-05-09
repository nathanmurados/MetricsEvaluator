using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class InteractionPermissionToggler : IInteractionPermissionToggler
    {
        public IEnableGroupingEvaluator EnableGroupingEvaluator { get; private set; }

        public InteractionPermissionToggler(IEnableGroupingEvaluator enableGroupingEvaluator)
        {
            EnableGroupingEvaluator = enableGroupingEvaluator;
        }

        public void Toggle(bool allow, ViewModel viewModel)
        {
            viewModel.AllowFolderChanges = allow;
            viewModel.IsIdle = allow;
            viewModel.EnableGroupSelecting = EnableGroupingEvaluator.Evaluate(viewModel);
            viewModel.HasLastFilesAndIsIdle = allow;
        }
    }
}