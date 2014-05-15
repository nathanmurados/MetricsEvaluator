namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IJsFileNameEvaluator
    {
        RefactoredFileNameViewModel Evaluate(string solutionRouteDirectory, string generatedResultDirectory, string originalFileName, int fragment);
    }
}