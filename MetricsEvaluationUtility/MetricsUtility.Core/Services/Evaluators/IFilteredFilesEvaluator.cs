using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators
{
    public interface IFilteredFilesEvaluator
    {
        List<string> Evaluate(IEnumerable<string> files);
        List<string> EvaluateFilteredExtensions();
    }
}