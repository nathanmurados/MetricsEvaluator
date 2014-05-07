using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class BoolOptionPresenter : IBoolOptionPresenter
    {
        public void Present(object sender, BoolOptionEventArgs e)
        {
            switch (MessageBox.Show(e.Question, "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No))
            {
                case MessageBoxResult.Yes: if (e.ActionOnTrue != null) { e.ActionOnTrue(); } break;
                case MessageBoxResult.No: if (e.ActionOnFalse != null) { e.ActionOnFalse(); } break;
            }
        }
    }
}