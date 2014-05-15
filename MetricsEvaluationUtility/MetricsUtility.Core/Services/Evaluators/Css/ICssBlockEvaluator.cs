using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssBlockEvaluator
    {
        List<DetailedCssEvaluationResult> Evaluate(string content);
    }
}