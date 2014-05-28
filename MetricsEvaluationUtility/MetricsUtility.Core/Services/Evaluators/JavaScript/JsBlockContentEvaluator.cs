using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsBlockContentEvaluator : BlockContentEvaluator, IJsBlockContentEvaluator
    {
        public JsBlockContentEvaluator(IRemediatedBlockJsRemover remediatedBlockJsRemover)
        {
            RemediatedBlockJsRemover = remediatedBlockJsRemover;
        }

        public BlockContent[] Evaluate(string[] contents, PageEvaluationMode mode, bool mergeBlocks)
        {
            return Split(contents, RegexConstants.ScriptOpeningTag, RegexConstants.ScriptClosingTag, mode, mergeBlocks);
        }
    }
}