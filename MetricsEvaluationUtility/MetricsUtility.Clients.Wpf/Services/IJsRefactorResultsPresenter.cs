using System.Collections.Generic;

namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IJsRefactorResultsPresenter
    {
        void Present(List<string> failedFiles, List<string> avoidedOverWrites);
    }
}