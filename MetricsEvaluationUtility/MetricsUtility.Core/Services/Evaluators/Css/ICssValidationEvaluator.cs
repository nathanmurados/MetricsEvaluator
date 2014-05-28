using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssValidationEvaluator
    {
        CssEvaluationResult Evaluate(string fileName, string[] contents, bool mergeBlocks);
    }
}