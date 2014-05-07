using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class InputPresenter : IInputPresenter
    {
        public void Present(object sender, string s, ViewModel dataContext)
        {
            MessageBox.Show("Get input now");
        }
    }
}