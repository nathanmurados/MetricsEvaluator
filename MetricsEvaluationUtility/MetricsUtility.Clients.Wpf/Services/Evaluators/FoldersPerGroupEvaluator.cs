using System;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class FoldersPerGroupEvaluator : IFoldersPerGroupEvaluator
    {
        public int Evaluate(int directoryCount, int numberOfGroups)
        {
            return (int)Math.Round((double)directoryCount / numberOfGroups);
        }
    }
}