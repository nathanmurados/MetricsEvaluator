using MetricsUtility.Core.Constants.Enums;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssBlockContentEvaluator
    {
        BlockContent[] Split(string[] lines, JsPageEvaluationMode mode);
    }
}