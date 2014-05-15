namespace MetricsUtility.Core.Services.Refactorers
{
    public class SolutionRelativeDirectoryEvaluator : ISolutionRelativeDirectoryEvaluator
    {
        public string Evaluate(string solutionDirectory, string newDirectory)
        {
            return newDirectory.Replace(solutionDirectory, "~").Replace("\\","/");
        }
    }
}