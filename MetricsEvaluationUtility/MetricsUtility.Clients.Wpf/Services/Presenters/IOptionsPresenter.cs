using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public interface IOptionsPresenter
    {
        void AddOption(object sender, AddOptionEventArgs addOptionEventArgs, ViewModel dataContext);
        void AddOptionWithHeadingSpace(object sender, AddOptionEventArgs addOptionEventArgs, ViewModel dataContext);
        void DisplayOptions(object sender, string s, ViewModel dataContext);
    }
}