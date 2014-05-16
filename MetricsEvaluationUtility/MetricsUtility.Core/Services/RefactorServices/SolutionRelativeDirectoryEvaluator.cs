namespace MetricsUtility.Core.Services.RefactorServices
{
    public class SolutionRelativeDirectoryEvaluator : ISolutionRelativeDirectoryEvaluator
    {
        public string Evaluate(string solutionDirectory, string newDirectory)
        {
            return newDirectory.Replace(solutionDirectory, "~").Replace("\\","/");
        }
    }
}