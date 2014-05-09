using System.Linq;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class HasFilesToInspectAndIsIdleEvaluator : IHasFilesToInspectAndIsIdleEvaluator
    {
        public HasFilesToInspectAndIsIdleEvaluator(IFilesToInspectEvaluator filesToInspectEvaluator)
        {
            FilesToInspectEvaluator = filesToInspectEvaluator;
        }

        public IFilesToInspectEvaluator FilesToInspectEvaluator { get; private set; }

        public bool Evaluate()
        {
            return FilesToInspectEvaluator.Evaluate().Any();
        }
    }
}