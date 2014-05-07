namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IFoldersPerGroupEvaluator
    {
        int Evaluate(int directoryCount, int numberOfGroups);
    }
}