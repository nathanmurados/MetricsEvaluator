using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtility.Clients.Wpf.Services
{
    public interface ISplitJsFileCreator
    {
        void Create(SeperatedJs seperatedJs, string newPath, List<string> avoidedOverWrites, ref int filesCreated, string file);
    }
}