namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsVariableNameEvaluator
    {
        string Evaluate(string razorCode);
    }
}
