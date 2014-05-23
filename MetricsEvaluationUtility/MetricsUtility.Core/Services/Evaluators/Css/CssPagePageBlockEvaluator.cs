using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class CssBlockContentEvaluator : BlockContentEvaluator, ICssBlockContentEvaluator
    {
        /// <summary>
        /// Derived from http://stackoverflow.com/questions/1079423/regular-expression-to-get-an-attribute-from-html-tag
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public BlockContent[] Split(string[] lines, JsPageEvaluationMode mode)
        {
            return Split(lines, RegexConstants.StyleOpeningTag, RegexConstants.StyleClosingTag, mode);
        }
    }
}