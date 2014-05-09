using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IInteractionPermissionToggler
    {
        void Toggle(bool allow, ViewModel viewModel);
    }
}