using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class HasRefactorPathsEvaluator : IHasRefactorPathsEvaluator
    {
        public bool Evaluate()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.GeneratedFilesPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.RefactorPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.SolutionPath);
        }
    }
}