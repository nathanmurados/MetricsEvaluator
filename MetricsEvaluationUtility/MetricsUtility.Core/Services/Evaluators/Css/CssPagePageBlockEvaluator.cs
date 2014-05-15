using System.Collections.Generic;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class CssPagePageBlockSplitter : PageBlockSplitter, ICssPageBlockSplitter
    {
        /// <summary>
        /// Derived from http://stackoverflow.com/questions/1079423/regular-expression-to-get-an-attribute-from-html-tag
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="includeBlocksWithAtVars"></param>
        /// <returns></returns>
        public List<PageBlockSplitResult> Split(string[] lines, bool includeBlocksWithAtVars)
        {
            return Split(lines, RegexConstants.StyleOpeningTag, RegexConstants.StyleClosingTag, includeBlocksWithAtVars);
        }
    }
}