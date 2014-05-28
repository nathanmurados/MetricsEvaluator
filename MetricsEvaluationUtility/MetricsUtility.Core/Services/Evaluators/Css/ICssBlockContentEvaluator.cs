using MetricsUtility.Core.Constants.Enums;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssBlockContentEvaluator
    {
        BlockContent[] Split(string[] lines, PageEvaluationMode mode, bool mergeBlocks);
    }
}