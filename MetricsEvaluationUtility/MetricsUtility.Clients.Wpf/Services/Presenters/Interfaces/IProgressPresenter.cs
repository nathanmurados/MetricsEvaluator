using System;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces
{
    public interface IProgressPresenter
    {
        void Present(object sender, int e, ViewModel viewModel);
        void Reset(object sender, EventArgs eventArgs, ViewModel viewModel);
    }
}