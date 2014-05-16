namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface ICssFileNameEvaluator
    {
        RefactoredFileNameViewModel Evaluate(string solutionDirectory, string newDirectory, string fileName, int fragment);
    }
}