using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsBlockContentEvaluator
    {
        BlockContent[] Evaluate(string[] contents, PageEvaluationMode mode, bool mergeBlocks);
    }
}