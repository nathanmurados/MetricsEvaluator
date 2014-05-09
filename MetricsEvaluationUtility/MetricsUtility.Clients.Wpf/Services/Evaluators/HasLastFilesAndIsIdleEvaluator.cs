using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    internal class HasLastFilesAndIsIdleEvaluator : IHasLastFilesAndIsIdleEvaluator
    {
        public bool Evaluate()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.LastFiles);
        }
    }
}