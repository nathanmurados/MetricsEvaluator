using System.Collections.Generic;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsPageEvaluator
    {
        List<PageBlockSplitResult> Evaluate(string[] contents, JsPageEvaluationMode mode);
    }
}