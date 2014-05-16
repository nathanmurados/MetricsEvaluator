using System.Collections.Generic;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsBlockContentEvaluator : BlockContentEvaluator, IJsBlockContentEvaluator
    {
        public BlockContent[] Evaluate(string[] contents, JsPageEvaluationMode mode)
        {
            return Split(contents, RegexConstants.ScriptOpeningTag, RegexConstants.ScriptClosingTag, mode);
        }
    }
}