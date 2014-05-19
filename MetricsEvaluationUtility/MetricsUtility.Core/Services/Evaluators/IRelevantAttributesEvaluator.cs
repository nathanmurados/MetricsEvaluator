using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;
namespace MetricsUtility.Core.Services.Evaluators
{
    
    public interface IRelevantAttributesEvaluator
    {
        List<string> Evaluate(List<JavaScriptEvaluationResult> results);
    }
}