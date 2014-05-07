using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public interface IOutputPresenter
    {
        void Write(object sender, string e, ViewModel viewModel);
        void WriteLine(object sender, string s, ViewModel viewModel);
    }
}