namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IJsSeperationService
    {
        SeperatedJs Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file);
    }
}