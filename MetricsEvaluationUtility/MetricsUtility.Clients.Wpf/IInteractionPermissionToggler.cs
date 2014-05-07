using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf
{
    public interface IInteractionPermissionToggler
    {
        void Toggle(bool allow, ViewModel viewModel);
    }
}