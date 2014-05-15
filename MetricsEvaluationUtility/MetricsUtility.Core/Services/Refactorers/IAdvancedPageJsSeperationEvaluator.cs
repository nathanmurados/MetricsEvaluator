namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IAdvancedPageJsSeperationEvaluator
    {
        SeperatedJsViewModel Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file);
    }
}