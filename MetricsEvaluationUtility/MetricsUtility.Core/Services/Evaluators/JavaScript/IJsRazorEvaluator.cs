using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsRazorEvaluator
    {
        List<DetailedJavaScriptEvaluationResult> Evaluate(string content, IEnumerable<string> attributes);
    }
}