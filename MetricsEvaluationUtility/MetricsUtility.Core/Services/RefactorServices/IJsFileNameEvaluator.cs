namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IJsFileNameEvaluator
    {
        RefactoredFileNameViewModel Evaluate(string solutionRouteDirectory, string generatedResultDirectory, string originalFileName, int fragment);
    }
}