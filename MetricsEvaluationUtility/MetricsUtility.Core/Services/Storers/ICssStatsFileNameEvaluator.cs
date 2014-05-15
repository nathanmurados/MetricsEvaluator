namespace MetricsUtility.Core.Services.Storers
{
    public interface ICssStatsFileNameEvaluator
    {
        string Evaluate();
        string Evaluate(string groupName);
    }
}