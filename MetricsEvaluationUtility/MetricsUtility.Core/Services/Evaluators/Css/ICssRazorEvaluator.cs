using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssRazorEvaluator
    {
        List<DetailedCssEvaluationResult> Evaluate(string content);
    }
}