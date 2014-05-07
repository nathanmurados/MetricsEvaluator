using System;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class OutputPresenter : IOutputPresenter
    {
        public void Write(object sender, string e, ViewModel viewModel)
        {
            viewModel.Output += e;
        }

        public void WriteLine(object sender, string e, ViewModel viewModel)
        {
            (viewModel).Output += string.Format("{0}{1}", e, Environment.NewLine);
        }
    }
}