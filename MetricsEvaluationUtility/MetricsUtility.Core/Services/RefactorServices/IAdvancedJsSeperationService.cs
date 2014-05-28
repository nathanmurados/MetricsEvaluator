namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IAdvancedJsSeperationService
    {
        SeperatedJs Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file, bool mergeBlocks);
    }
}