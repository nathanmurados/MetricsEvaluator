using System.Collections.Generic;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Clients.Wpf.Services
{
    public interface ISplitJsFileCreator
    {
        void Create(SeperatedJsViewModel seperatedJsViewModel, string newPath, List<string> avoidedOverWrites, ref int filesCreated, string file);
    }
}