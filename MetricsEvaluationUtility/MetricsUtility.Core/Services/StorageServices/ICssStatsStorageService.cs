using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.StorageServices
{
    public interface ICssStatsStorageService
    {
        string Store(List<CssEvaluationResult> results, string groupName);
    }
}