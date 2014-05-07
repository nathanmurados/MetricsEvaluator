using System.Collections.Generic;

namespace MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces
{
    public interface IJavaScriptMetricsPresenter
    {
        void View(List<string> files);
    }
}