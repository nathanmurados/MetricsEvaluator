using System.Collections.Generic;
using MetricsUtility.Core.Enums;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssBlockContentEvaluator
    {
        BlockContent[] Split(string[] lines, JsPageEvaluationMode mode);
    }
}