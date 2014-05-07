using System.IO;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class EnableDiagnosticsEvaluator : IEnableDiagnosticsEvaluator
    {
        public bool Evaluate()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.InspectionPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.ResultsPath)
                   && Directory.Exists(Properties.Settings.Default.InspectionPath)
                   && Directory.Exists(Properties.Settings.Default.ResultsPath);
        }
    }
}