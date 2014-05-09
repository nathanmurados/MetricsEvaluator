using System;
using System.Linq;
using MetricsUtility.Core.Services.Extensions;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class CssValidationEvaluator : ICssValidationEvaluator
    {
        public ICssRazorEvaluator CssRazorEvaluator { get; private set; }
        public ICssPageEvaluator CssPageEvaluator { get; private set; }
        public ICssBlockEvaluator CssBlockEvaluator { get; private set; }

        public CssValidationEvaluator(ICssBlockEvaluator cssBlockEvaluator, ICssRazorEvaluator cssRazorEvaluator, ICssPageEvaluator cssPageEvaluator)
        {
            CssBlockEvaluator = cssBlockEvaluator;
            CssPageEvaluator = cssPageEvaluator;
            CssRazorEvaluator = cssRazorEvaluator;
        }

        public CssEvaluationResult Evaluate(string filename, string[] contents)
        {
            var joinedString = string.Join("", contents);

            if (!joinedString.Contains("style", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var page = CssPageEvaluator.Evaluate(contents);
            var block = CssBlockEvaluator.Evaluate(joinedString);
            var razor = CssRazorEvaluator.Evaluate(joinedString);

            if (page.Any() || block.Any() || razor.Any())
            {
                return new CssEvaluationResult
                {
                    FileName = filename,
                    Page = page,
                    Inline = block,
                    Razor = razor
                };
            }

            return null;
        }
    }
}