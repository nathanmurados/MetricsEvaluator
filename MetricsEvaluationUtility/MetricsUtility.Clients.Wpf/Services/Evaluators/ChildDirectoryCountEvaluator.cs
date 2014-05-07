using System.IO;
using System.Linq;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ChildDirectoryCountEvaluator : IChildDirectoryCountEvaluator
    {
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }

        public ChildDirectoryCountEvaluator(IPathExistenceEvaluator pathExistenceEvaluator)
        {
            PathExistenceEvaluator = pathExistenceEvaluator;
        }
        
        public int Evaluate()
        {
            return PathExistenceEvaluator.Evaluate(Properties.Settings.Default.InspectionPath) ? Directory.GetDirectories(Properties.Settings.Default.InspectionPath).Count() : 0;
        }
    }
}