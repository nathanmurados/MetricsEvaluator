using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.StorageServices
{
    public class JavaScriptStatsStorageService : IJavaScriptStatsStorageService, IHasHumanInterface, IHasDateTimeProvider
    {
        public IRelevantAttributesEvaluator RelevantAttributesEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IStorageService StorageService { get; private set; }
        public IJavaScriptStatsFileNameEvaluator JavaScriptStatsFileNameEvaluator { get; private set; }

        public JavaScriptStatsStorageService(IStorageService storageService, IDateTimeProvider dateTimeProvider, IHumanInterface ux, IRelevantAttributesEvaluator relevantAttributesEvaluator, IJavaScriptStatsFileNameEvaluator javaScriptStatsFileNameEvaluator)
        {
            JavaScriptStatsFileNameEvaluator = javaScriptStatsFileNameEvaluator;
            StorageService = storageService;
            DateTimeProvider = dateTimeProvider;
            Ux = ux;
            RelevantAttributesEvaluator = relevantAttributesEvaluator;
        }

        public string Store(List<JavaScriptEvaluationResult> results, string groupName)
        {
            var attributeTotals = new List<AttributeTotal>();

            var sb = new StringBuilder();

            sb.Append("Filename,Inline Instances,Total Inline LOC, Contains '@'");

            var comparer = StringComparer.OrdinalIgnoreCase;

            var attributesInUse = RelevantAttributesEvaluator.Evaluate(results);

            foreach (var attribute in attributesInUse)
            {
                sb.AppendFormat(",{0}", attribute);
            }

            sb.AppendLine(",Total Inline Instances,Total Razor Instances");

            foreach (var result in results.OrderBy(x => x.FileName))
            {
                sb.AppendFormat("{0},{1},{2},{3}",
                    result.FileName, 
                    result.PageInstances.Length, 
                    result.PageInstances.Sum(x => x.Lines.Count), result.PageInstances.Any(x => x.AtSymbols > 0));

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

            sb.AppendFormat("Total: {0},{1},{2},{3}",
                results.Count,
                results.Sum(x => x.PageInstances.Length),
                results.Sum(x => x.PageInstances.Sum(y => y.Lines.Count)),
                results.Sum(x => x.PageInstances.Count(y => y.AtSymbols > 0)));


            foreach (var attribute in attributesInUse)
            {
                sb.AppendFormat(",{0}", attributeTotals.Where(x => x.Attribute == attribute).Sum(x => x.BlockCount + x.RazorCount));
            }

            sb.AppendFormat(",{0},{1}", attributeTotals.Sum(x => x.BlockCount), attributeTotals.Sum(x => x.RazorCount));

            var filename = StorageService.Store(sb, JavaScriptStatsFileNameEvaluator.Evaluate(groupName));

            Ux.WriteLine(string.Format("Saved to {0}", filename));
            Ux.WriteLine("");

            return filename;
        }

    }

    internal class AttributeTotal
    {
        public string Attribute { get; set; }
        public int BlockCount { get; set; }
        public int RazorCount { get; set; }
    }

    public interface IJavaScriptStatsStorageService
    {
        string Store(List<JavaScriptEvaluationResult> results, string groupName);
    }
}