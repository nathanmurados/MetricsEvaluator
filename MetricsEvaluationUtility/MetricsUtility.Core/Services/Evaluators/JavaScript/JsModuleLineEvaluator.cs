using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
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
            // I'm assuming the fragment is surrounded by quotes

            string fragment;
            
            int atPosition = jsLine.IndexOf("@");

            string firstDelimiterCharacter = jsLine.Substring(atPosition - 1, 1);   // take character to the left

            // This is a HACK to cater for non string razor like below
            // example: "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));"
            // The dodgy assumption here is that the line is terminated with a ;
            if (firstDelimiterCharacter == " ")
            {
                int endDelimierPosition = jsLine.LastIndexOf(";");
                fragment = jsLine.Substring(atPosition, endDelimierPosition - atPosition); // don't include delimiters

            }
            else
            {
                // I'm assuming the delimiter is a quote, single or double, and that there is a matching end quote
                // example: "$('#DecommisionReason').val('@decommisionReason');"
                int lastQuotePosition = jsLine.LastIndexOf(firstDelimiterCharacter);

                fragment = jsLine.Substring(atPosition - 1, lastQuotePosition - atPosition + 2); // include the quote delimiters
            }

            

            output.Add(fragment);
            
            return output;
        }
    }
}
