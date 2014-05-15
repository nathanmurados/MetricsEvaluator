namespace MetricsUtility.Core.Services.Refactorers
{
    public interface ISolutionRelativeDirectoryEvaluator
    {
        string Evaluate(string solutionDirectory, string newDirectory);
    }
}