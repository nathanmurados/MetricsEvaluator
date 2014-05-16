using System.Collections.Generic;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsBlockContentEvaluator
    {
        BlockContent[] Evaluate(string[] contents, JsPageEvaluationMode mode);
    }
}