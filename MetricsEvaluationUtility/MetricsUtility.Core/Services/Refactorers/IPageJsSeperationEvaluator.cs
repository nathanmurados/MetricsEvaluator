namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IPageJsSeperationEvaluator
    {
        SeperatedJsViewModel Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file);
    }
}