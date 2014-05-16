namespace MetricsUtility.Core.Services.StorageServices
{
    public interface IJavaScriptStatsFileNameEvaluator
    {
        string Evaluate(string groupName);
    }
}