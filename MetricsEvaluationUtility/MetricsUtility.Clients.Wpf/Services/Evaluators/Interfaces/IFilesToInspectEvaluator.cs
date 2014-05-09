using System.Collections.Generic;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IFilesToInspectEvaluator
    {
        List<string> Evaluate();
    }
}