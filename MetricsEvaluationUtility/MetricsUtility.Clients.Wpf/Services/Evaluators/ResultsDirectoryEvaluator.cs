using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ResultsDirectoryEvaluator : IResultsDirectoryEvaluator
    {
        public string Evaluate()
        {
            return Properties.Settings.Default.ResultsDirectory;
        }
    }
}