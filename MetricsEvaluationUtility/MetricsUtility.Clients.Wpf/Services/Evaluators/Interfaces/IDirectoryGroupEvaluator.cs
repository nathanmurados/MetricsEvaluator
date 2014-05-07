using System.Collections.Generic;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IDirectoryGroupEvaluator
    {
        List<List<string>> Evaluate(int numberOfGroups, string[] directories);
    }
}