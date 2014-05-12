using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class HasCssRefactorPathsEvaluator : IHasCssRefactorPathsEvaluator
    {
        public bool Evaluate()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.GeneratedCssPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.RefactorCssPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.SolutionPath);
        }
    }
}