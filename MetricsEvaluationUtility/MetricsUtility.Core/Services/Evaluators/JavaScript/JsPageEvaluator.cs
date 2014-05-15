using System.Collections.Generic;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsPageEvaluator :PageBlockSplitter, IJsPageEvaluator
    {
        public List<PageBlockSplitResult> Evaluate(string[] contents, JsPageEvaluationMode mode)
        {
            return Split(contents, RegexConstants.ScriptOpeningTag, RegexConstants.ScriptClosingTag, mode);
        }
    }
}