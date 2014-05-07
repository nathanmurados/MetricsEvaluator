using System.Windows;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class OptionsPresenter : IOptionsPresenter
    {
        public void AddOption(object sender, AddOptionEventArgs addOptionEventArgs, ViewModel dataContext)
        {
            MessageBox.Show("Add option now");
        }

        public void AddOptionWithHeadingSpace(object sender, AddOptionEventArgs addOptionEventArgs, ViewModel dataContext)
        {
            MessageBox.Show("Add option with heading space now");
        }

        public void DisplayOptions(object sender, string s, ViewModel dataContext)
        {
            MessageBox.Show("Display option now");
        }
    }
}