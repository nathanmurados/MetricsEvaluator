using System;
using System.Linq;
using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Extensions;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class CssValidationEvaluator : ICssValidationEvaluator
    {
        public ICssRazorEvaluator CssRazorEvaluator { get; private set; }
        public ICssBlockContentEvaluator CssBlockContentEvaluator { get; private set; }
        public ICssBlockEvaluator CssBlockEvaluator { get; private set; }

        public CssValidationEvaluator(ICssBlockEvaluator cssBlockEvaluator, ICssRazorEvaluator cssRazorEvaluator, ICssBlockContentEvaluator cssBlockContentEvaluator)
        {
            CssBlockEvaluator = cssBlockEvaluator;
            CssBlockContentEvaluator = cssBlockContentEvaluator;
            CssRazorEvaluator = cssRazorEvaluator;
        }

        public CssEvaluationResult Evaluate(string filename, string[] contents, bool mergeBlocks)
        {
            var joinedString = string.Join("", contents);

            if (!joinedString.Contains("style", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var page = CssBlockContentEvaluator.Split(contents, PageEvaluationMode.Any,mergeBlocks);
            var block = CssBlockEvaluator.Evaluate(joinedString);
            var razor = CssRazorEvaluator.Evaluate(joinedString);

            if (page.Any() || block.Any() || razor.Any())
            {
                return new CssEvaluationResult
                {
                    FileName = filename,
                    Page = page.Select(x=> x.Lines.Count()).ToList(),
                    Inline = block,
                    Razor = razor
                };
            }

            return null;
        }
    }
}