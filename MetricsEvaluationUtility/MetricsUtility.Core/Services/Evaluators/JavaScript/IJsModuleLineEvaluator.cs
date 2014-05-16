namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsModuleLineEvaluator
    {
        string[] Evaluate(string jsLine);
    }
}
