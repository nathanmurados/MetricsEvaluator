using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsValidationEvaluator
    {
        JavaScriptEvaluationResult Evaluate(string filename, string[] contents, List<string> attributes);
    }
}