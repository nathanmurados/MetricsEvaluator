using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public interface IInputPresenter
    {
        void Present(object sender, string s, ViewModel dataContext);
    }
}