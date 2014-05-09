namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class HasCssRefactorPathsEvaluator : IHasCssRefactorPathsEvaluator
    {
        public bool Evaluate()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.GeneratedCssPath)
                   && !string.IsNullOrWhiteSpace(Properties.Settings.Default.RefactorCssPath);
        }
    }
}