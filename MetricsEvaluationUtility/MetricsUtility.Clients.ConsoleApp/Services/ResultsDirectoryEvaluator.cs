using MetricsUtility.Core.Services.StorageServices;

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