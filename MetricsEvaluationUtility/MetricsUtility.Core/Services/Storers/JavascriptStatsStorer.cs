using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Storers
{
    public class JavaScriptStatsStorer : IJavaScriptStatsStorer, IHasHumanInterface, IHasDateTimeProvider
    {
        public IRelevantAttributesEvaluator RelevantAttributesEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IStorer Storer { get; private set; }

        public JavaScriptStatsStorer(IStorer storer, IDateTimeProvider dateTimeProvider, IHumanInterface ux, IRelevantAttributesEvaluator relevantAttributesEvaluator)
        {
            Storer = storer;
            DateTimeProvider = dateTimeProvider;
            Ux = ux;
            RelevantAttributesEvaluator = relevantAttributesEvaluator;
        }

        public void Store(List<JavaScriptEvaluationResult> results)
        {
            var attributeTotals = new List<AttributeTotal>();

            var sb = new StringBuilder();

            sb.Append("Filename,Inline Instances,Total Inline LOC");

            var comparer = StringComparer.OrdinalIgnoreCase;

            var attributesInUse = RelevantAttributesEvaluator.Evaluate(results);

            foreach (var attribute in attributesInUse)
            {
                sb.AppendFormat(",{0}", attribute);
            }

            sb.AppendLine(",Total Inline Instances,Total Razor Instances");

            foreach (var result in results)
            {
                sb.AppendFormat("{0},{1},{2}", result.FileName, result.PageInstances.Count, result.PageInstances.Sum(x => x));

                foreach (var attribute in attributesInUse)
                {
                    var blockCount = result.Block.Where(x => comparer.Equals(attribute, x.AttributeName)).Sum(x => x.InlineJavascriptTags.Count);
                    var razorCount = result.Razor.Count(x => comparer.Equals(attribute, x.AttributeName));
                    sb.AppendFormat(",{0}", blockCount + razorCount);

                    attributeTotals.Add(new AttributeTotal
                    {
                        Attribute = attribute,
                        BlockCount = blockCount,
                        RazorCount = razorCount
                    });
                }

                sb.AppendFormat(",{0},{1}", result.Block.Sum(x => x.InlineJavascriptTags.Count), result.Razor.Count);
                sb.AppendLine("");
            }

            sb.AppendFormat("Total: {2},{0},{1}", results.Sum(x => x.PageInstances.Count), results.Sum(x => x.PageInstances.Sum(y => y)), results.Count);

            foreach (var attribute in attributesInUse)
            {
                sb.AppendFormat(",{0}", attributeTotals.Where(x => x.Attribute == attribute).Sum(x => x.BlockCount + x.RazorCount));
            }

            sb.AppendFormat(",{0},{1}", attributeTotals.Sum(x => x.BlockCount), attributeTotals.Sum(x => x.RazorCount));

            Storer.Store(sb, "JavaScriptValidationResults " + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv");

            Ux.WriteLine("Saved.");
        }

    }

    internal class AttributeTotal
    {
        public string Attribute { get; set; }
        public int BlockCount { get; set; }
        public int RazorCount { get; set; }
    }

    public interface IJavaScriptStatsStorer
    {
        void Store(List<JavaScriptEvaluationResult> results);
    }
}