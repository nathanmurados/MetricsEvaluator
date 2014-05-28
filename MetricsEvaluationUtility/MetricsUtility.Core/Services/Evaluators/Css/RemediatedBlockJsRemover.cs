using System.Collections.Generic;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class RemediatedBlockJsRemover : IRemediatedBlockJsRemover
    {
        public List<BlockContent> Remove(IEnumerable<BlockContent> matches)
        {
            // Throw out any existing ap2 blocks that were added during the manual phase
            var filteredMatches = new List<BlockContent>();
            foreach (var block in matches)
            {
                var containsModuleSyntax = block.Lines.Any(x =>
                    x.IndexOf("var ap2 = (function", System.StringComparison.OrdinalIgnoreCase) > 0
                 || x.IndexOf("(ap2 || {}));", System.StringComparison.OrdinalIgnoreCase) > 0
                 || x.IndexOf("(ap2 || {}));", System.StringComparison.OrdinalIgnoreCase) > 0
                 || x.IndexOf("ap2.", System.StringComparison.OrdinalIgnoreCase) > 0
                );

                var containsClosedBraces = block.Lines.Any(x => x.Contains("{}"));
                var numberOfActualLines = block.Lines.Count(x => !string.IsNullOrWhiteSpace(x.Trim())) - 2; //close / start tags
                var allAtSignsPreceededByEquals = block.Lines.Where(x => x.Contains("@")).All(x => x.IndexOf("=", System.StringComparison.OrdinalIgnoreCase) < x.IndexOf("@", System.StringComparison.OrdinalIgnoreCase));
                var hasRightNumberOfAtSigns = block.AtSymbols == (numberOfActualLines - 3);
                var razorLinecount = block.Lines.Count(x => x.Contains("@"));

                var atSaturation = (100.0 / numberOfActualLines) * razorLinecount;

                if (containsModuleSyntax || atSaturation > 80)
                {
                    break;
                }

                if (hasRightNumberOfAtSigns)
                {
                    if (allAtSignsPreceededByEquals)
                    {
                        if (containsClosedBraces)
                        {
                            break;
                        }
                    }
                }

                filteredMatches.Add(block);

            }

            return filteredMatches;
        }
    }
}