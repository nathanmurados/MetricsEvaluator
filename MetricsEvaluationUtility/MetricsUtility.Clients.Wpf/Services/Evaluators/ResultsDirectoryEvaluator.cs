using MetricsUtility.Core.Services.StorageServices;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ResultsDirectoryEvaluator : IResultsDirectoryEvaluator
    {
        public string Evaluate()
        {
            return Properties.Settings.Default.ResultsPath;
        }
    }
}