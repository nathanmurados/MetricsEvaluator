using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Extensions;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsValidationEvaluator : IJsValidationEvaluator
    {
        public IJsRazorEvaluator JsRazorEvaluator { get; private set; }
        public IJsBlockContentEvaluator JsBlockContentEvaluator { get; private set; }
        public IJsBlockEvaluator JsBlockEvaluator { get; private set; }
        public IJsReferencesEvaluator JsReferencesEvaluator { get; private set; }

        public JsValidationEvaluator(IJsBlockEvaluator jsBlockEvaluator, IJsBlockContentEvaluator jsBlockContentEvaluator, IJsRazorEvaluator jsRazorEvaluator, IJsReferencesEvaluator jsReferencesEvaluator)
        {
            JsReferencesEvaluator = jsReferencesEvaluator;
            JsBlockEvaluator = jsBlockEvaluator;
            JsBlockContentEvaluator = jsBlockContentEvaluator;
            JsRazorEvaluator = jsRazorEvaluator;
        }


        public JavaScriptEvaluationResult Evaluate(string filename, string[] contents, List<string> attributes, bool mergeBlocks)
        {
            var joinedString = string.Join("", contents);

            if ((!joinedString.Contains("script", StringComparison.OrdinalIgnoreCase) && attributes.Any(x => joinedString.Contains(x, StringComparison.OrdinalIgnoreCase))))
            {
                return null;
            }

            var pageLevel = JsBlockContentEvaluator.Evaluate(contents, PageEvaluationMode.Any,mergeBlocks);
            var references = JsReferencesEvaluator.Evaluate(joinedString);
            var block = JsBlockEvaluator.Evaluate(joinedString, attributes);
            var razor = JsRazorEvaluator.Evaluate(joinedString, attributes);

            if (pageLevel.Any() || references > 0 || block.Any() || razor.Any())
            {
                return new JavaScriptEvaluationResult
                {
                    FileName = filename,
                    PageInstances = pageLevel,
                    References = references,
                    Block = block,
                    Razor = razor,
                };
            }

            return null;
        }
    }
}