namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IAdvancedJsSeperationService
    {
        SeperatedJs Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file);
    }
}