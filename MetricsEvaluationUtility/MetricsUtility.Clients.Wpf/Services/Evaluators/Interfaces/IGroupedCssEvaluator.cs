namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IGroupedCssEvaluator
    {
        void Evaluate(int numberOfGroups, string[] directories);
    }
}