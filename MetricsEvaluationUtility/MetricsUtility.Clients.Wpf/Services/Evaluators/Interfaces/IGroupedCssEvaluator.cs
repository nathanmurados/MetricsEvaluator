using System;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IGroupedCssEvaluator
    {
        event EventHandler ScrollDown;

        void Evaluate(int numberOfGroups, string[] directories);
    }
}