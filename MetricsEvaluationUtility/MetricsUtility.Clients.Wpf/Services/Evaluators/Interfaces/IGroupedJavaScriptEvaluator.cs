using System;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IGroupedJavaScriptEvaluator
    {
        event EventHandler ScrollDown;

        void Evaluate(int numberOfGroups, string[] directories, int specificGroup);
    }
}