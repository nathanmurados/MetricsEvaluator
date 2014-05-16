using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators
{
    public class RelevantAttributesEvaluator : IRelevantAttributesEvaluator
    {
        public List<string> Evaluate(List<JavaScriptEvaluationResult> results)
        {
            var attributesInUse = new List<string>();

            var comparer = StringComparer.OrdinalIgnoreCase;

            foreach (var result in results)
            {
                foreach (var block in result.Block.Where(block => !attributesInUse.Any(x => comparer.Equals(x, block.AttributeName))))
                {
                    attributesInUse.Add(block.AttributeName);
                }

                foreach (var razor in result.Razor.Where(razor => !attributesInUse.Any(x => comparer.Equals(x, razor.AttributeName))))
                {
                    attributesInUse.Add(razor.AttributeName);
                }
            }

            return attributesInUse;
        }
    }
}