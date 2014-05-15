namespace MetricsUtility.Core.Services.Refactorers
{
    public interface ICssFileNameEvaluator
    {
        RefactoredFileNameViewModel Evaluate(string solutionDirectory, string newDirectory, string fileName, int fragment);
    }
}