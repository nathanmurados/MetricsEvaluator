using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces
{
    public interface IInputPresenter
    {
        void Present(object sender, string s, ViewModel dataContext);
    }
}