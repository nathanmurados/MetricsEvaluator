namespace MetricsUtility.Core.Services.Storers
{
    public interface IJavaScriptStatsFileNameEvaluator
    {
        string Evaluate(string groupName);
    }
}