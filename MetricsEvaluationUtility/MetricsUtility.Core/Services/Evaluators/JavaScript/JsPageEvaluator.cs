using System.Collections.Generic;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsPageEvaluator :PageBlockSplitter, IJsPageEvaluator
    {
        public List<PageBlockSplitResult> Evaluate(string[] contents, bool includeBlocksWithAtVars)
        {
            return Split(contents, RegexConstants.ScriptOpeningTag, RegexConstants.ScriptClosingTag, includeBlocksWithAtVars);
        }
    }

    public enum JsPageEvaluationMode
    {
        OnlyNonAtVars,
        OnlyAtVars,
        Any
    }
}