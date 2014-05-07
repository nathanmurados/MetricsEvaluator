using System;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class FoldersPerGroupEvaluator : IFoldersPerGroupEvaluator
    {
        public int Evaluate(int directoryCount, int numberOfGroups)
        {
            if (directoryCount == 0) return 0;

            return (int)Math.Round((double)directoryCount / numberOfGroups);
        }
    }
}