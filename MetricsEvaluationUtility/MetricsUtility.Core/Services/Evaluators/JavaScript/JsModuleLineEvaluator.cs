using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsModuleLineEvaluator : IJsModuleLineEvaluator
    {
        /// <summary>
        /// Extract the razor code fragment from a line of Javascript.
        /// Input: A line of Javascript containing an @. The @ prefixes the razor code.
        /// Note. the line may contain several fragments of razor.
        /// The razor code is prefixed with @, it will probably be surrounded by quotes (single or double), but not always!
        /// </summary>
        public List<Fragment> Evaluate(string jsLine)
        {
            // we can't handle lines containing "@Url
            if (jsLine.Contains("\"@Url"))
            {
                throw new UnhandledPatternException(jsLine);
            }


            List<Fragment> output = new List<Fragment>();

            // Multi fragment line?
            if (jsLine.ToCharArray().Count(c => c == '@') > 1)
            {
                return ProcessMultiFragmentLine(jsLine);
            }

            Fragment fragment = new Fragment();

            int atPosition = jsLine.IndexOf("@");

            char firstCharacterToLeft = jsLine.Substring(atPosition - 1, 1).ToCharArray()[0];   // take character to the left

            // Immediately Quoted? 
            // example: '@decommisionReason'
            if (Constants.CharacterConstants.Quotes.Contains(firstCharacterToLeft)) // is a quote?
            {
                int rightDelimeterPos = GetRightDelimiterPosition(jsLine, atPosition, firstCharacterToLeft);

                if (rightDelimeterPos > 0)
                {
                    fragment.Text = jsLine.Substring(atPosition - 1, rightDelimeterPos - atPosition + 2); // include the quote delimiters
                    fragment.FragType = FragType.Quoted;
                    output.Add(fragment);
                    return output;
                }
                // We can't find the matching quote, or a space was hit before hitting the right quote
                throw new UnhandledPatternException(jsLine);
            }

            // Non quoted?
            // example: globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));
            if (firstCharacterToLeft == ' ')
            {
                // if there's a quote, play safe and terminate
                if (jsLine.IndexOf("'", System.StringComparison.Ordinal) > 0)
                {
                    throw new UnhandledPatternException(jsLine);
                }

                // Assume its unquoted, but terminated by a ;
                int endDelimierPosition = jsLine.LastIndexOf(";");
                if (endDelimierPosition == -1) // is there a ; ?
                {
                    throw new UnhandledPatternException(jsLine);
                }

                fragment.Text = jsLine.Substring(atPosition, endDelimierPosition - atPosition); // don't include delimiters
                fragment.FragType = FragType.Unquoted;
                output.Add(fragment);
                return output;
            }

            // Its not a pattern we are confident we recognize, it will have to be done manually
            throw new UncaughtPatternException();
        }

        private int GetRightDelimiterPosition(string jsLine, int atPosition, char delimiterChar)
        {
            bool tolerateSpace = false;

            // take text to right of the @
            string chunk = jsLine.Substring(atPosition + 1);
            int pos = atPosition + 1;

            if (jsLine.Contains("@Url"))
            {
                tolerateSpace = true;
            }

            // scan to right looking for a matching delimiter char, but if we find a space all bets are off
            foreach (char c in chunk)
            {
                if (c == ' ' && !tolerateSpace)
                {
                    return -1;
                }

                if (c == delimiterChar)
                {
                    return pos;
                }
                pos++; // we are moving on the the next char, so update its position
            }

            return -1;
        }

        private List<Fragment> ProcessMultiFragmentLine(string jsline)
        {
            // I'm assuming the follow pattern of input
            // data: "{'docId1':'" + '@ViewBag.docid' + "','conditionType1':'" + '@ViewBag.doctype' + "'}"

            List<Fragment> result = new List<Fragment>();

            int pos = 0;
            foreach (char c in jsline) // scan the line char by char
            {
                if (c == '@')
                {
                    string previousChar = jsline.Substring(pos - 1, 1);

                    if (previousChar == "'" || previousChar == "\"") // prefixed by quote ?
                    {
                        int endQuotePosition = GetEndQuotePosition(jsline, pos, previousChar);

                        Fragment fragment = new Fragment();
                        fragment.Text = jsline.Substring(pos - 1, endQuotePosition - pos + 2);
                        fragment.FragType = FragType.Quoted;
                        result.Add(fragment);
                    }
                    else
                    {
                        throw new UnhandledPatternException(jsline);
                    }
                }
                pos++;
            }

            return result;
        }

        private int GetEndQuotePosition(string jsline, int pos, string quoteChar)
        {
            int end = jsline.Length - 1;

            // scan from first quote position until we hit another quote

            while (pos < end && jsline.Substring(pos, 1) != quoteChar)
            {
                if (jsline.Substring(pos + 1, 1) == quoteChar)
                {
                    return pos + 1;
                }
                pos++;
            }

            throw new UnhandledPatternException(jsline);
        }
    }
}
