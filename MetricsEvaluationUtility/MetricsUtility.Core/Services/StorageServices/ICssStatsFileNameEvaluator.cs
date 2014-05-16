namespace MetricsUtility.Core.Services.StorageServices
{
    public interface ICssStatsFileNameEvaluator
    {
        string Evaluate();
        string Evaluate(string groupName);
    }
}