using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    using System.Linq;

    public class JsModuleLineEvaluator : IJsModuleLineEvaluator
    {
        /// <summary>
        /// Extract the razor code from a line of javascript.
        /// Input: A line of Javascript containing an @. The @ prefixes the razor code.
        /// Note the line may contain several fragments of razor.
        /// The razor code is prefixed with @, it will probably be surrounded by quotes (single or double), but not always!
        /// </summary>
        public List<string> Evaluate(string jsLine)
        {
            List<string> output = new List<string>();

            // this is a quick and dirty implementation

            if (jsLine.ToCharArray().Count(c => c == '@') > 1) // has more than one fragment
            {
                return ProcessMultiFragmentLine(jsLine);
            }

            string fragment;
            
            int atPosition = jsLine.IndexOf("@");

            string firstDelimiterCharacter = jsLine.Substring(atPosition - 1, 1);   // take character to the left

            // Cater for non quoted razor like below
            // example: "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));"
            // The dodgy assumption here is that the line is terminated with a ;
            //
            if (firstDelimiterCharacter == " ")
            {
                int endDelimierPosition = jsLine.LastIndexOf(";");
                if (endDelimierPosition == -1) // is there a ; ?
                {
                    return PatternNotHandled();
                }

                fragment = jsLine.Substring(atPosition, endDelimierPosition - atPosition); // don't include delimiters
            }
            else if (firstDelimiterCharacter == "'" || firstDelimiterCharacter == "\"")
            {
                // The delimiter is a quote, single or double, and I'm assuming there's a matching end quote
                // example: "$('#DecommisionReason').val('@decommisionReason');"
                //
                int lastQuotePosition = jsLine.LastIndexOf(firstDelimiterCharacter);

                fragment = jsLine.Substring(atPosition - 1, lastQuotePosition - atPosition + 2); // include the quote delimiters
            }
            else
            {
                return PatternNotHandled();
            }

            output.Add(fragment);
            
            return output;
        }

        private List<string> PatternNotHandled()
        {
            throw new System.NotImplementedException("Pattern not handled");
        }

        private List<string> ProcessMultiFragmentLine(string jsline)
        {
            // I'm assuming the follow pattern of input
            // data: "{'docId1':'" + '@ViewBag.docid' + "','conditionType1':'" + '@ViewBag.doctype' + "'}"

            List<string> result = new List<string>();
            string fragment;

            int pos = 0;
            foreach (char c in jsline) // scan the line char by char
            {
                if (c == '@')
                {
                    string previousChar = jsline.Substring(pos - 1, 1);

                    if (previousChar == "'" || previousChar == "\"") // prefixed by quote ?
                    {
                        int endQuotePosition = GetEndQuotePosition(jsline, pos, previousChar);
                        fragment = jsline.Substring(pos - 1, endQuotePosition - pos + 2);
                        result.Add(fragment);
                    }
                    else
                    {
                        return this.PatternNotHandled();
                    }
                }
                pos++;
            }

            return result;
        }

        private int GetEndQuotePosition(string jsline, int pos, string quoteChar)
        {
            int end = jsline.Length -1;

            // scan from first quote position until we hit another quote

            while (pos < end &&  jsline.Substring(pos, 1) != quoteChar)
            {
                if (jsline.Substring(pos + 1, 1) == quoteChar)
                {
                    return pos + 1;
                }
                pos++;
            }

            throw new System.NotImplementedException();
        }
    }
}
