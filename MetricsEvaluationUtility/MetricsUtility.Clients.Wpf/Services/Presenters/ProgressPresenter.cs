using System;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class ProgressPresenter : IProgressPresenter
    {
        public void Present(object sender, int e, ViewModel viewModel)
        {
            viewModel.ProgressValue = e;
        }

        public void Reset(object sender, EventArgs eventArgs, ViewModel viewModel)
        {
            viewModel.ProgressValue = 0;
        }
    }
}