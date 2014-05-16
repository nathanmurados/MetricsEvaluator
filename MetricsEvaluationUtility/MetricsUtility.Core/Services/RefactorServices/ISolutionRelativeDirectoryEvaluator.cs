namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface ISolutionRelativeDirectoryEvaluator
    {
        string Evaluate(string solutionDirectory, string newDirectory);
    }
}