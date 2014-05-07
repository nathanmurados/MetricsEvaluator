using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.ConsoleApp.Services
{
    public class ResultsDirectoryEvaluator : IResultsDirectoryEvaluator
    {
        public string Evaluate()
        {
            return @"C:\MetricsEvaluationUtility\";
        }
    }
}