using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf
{
    public class InteractionPermissionToggler : IInteractionPermissionToggler
    {
        public void Toggle(bool allow, ViewModel viewModel)
        {
            viewModel.AllowInteractions = allow;
            viewModel.EnableDiagnostics = allow;
        }
    }
}