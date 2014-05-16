namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IGetJsToRefactor
    {
        string[] Evaluate(string jsLine);
    }
}
