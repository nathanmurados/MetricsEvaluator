using System.Collections.Generic;
using MetricsUtility.Core.Enums;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssPageBlockSplitter
    {
        List<PageBlockSplitResult> Split(string[] lines, JsPageEvaluationMode mode);
    }
}