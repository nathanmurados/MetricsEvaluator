using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsEvaluationUtility.ViewModels;

namespace MetricsEvaluationUtility.Services.Evaluators.Css
{
    public class CssBlockEvaluator : ICssBlockEvaluator
    {
        public List<DetailedCssEvaluationResult> Evaluate(string content)
        {
            var regex = new Regex("<[^>]+\\s*style\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            var matches = regex.Matches(content);

            return (from Match match in matches
                select new DetailedCssEvaluationResult
                {
                    Value = match.Value
                }).ToList();
        }
    }

    public interface ICssBlockEvaluator
    {
        List<DetailedCssEvaluationResult> Evaluate(string content);
    }
}