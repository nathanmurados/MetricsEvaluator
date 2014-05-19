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
            
            //output.Add("'@fragment1'");
            //output.Add("'@fragment2'");

            // this is a quick and dirty implementation
            // I'm assuming the fragment is surrounded by quotes

            int atPosition = jsLine.IndexOf("@");
            string firstQuoteCharaacter = jsLine.Substring(atPosition - 1, 1);     // I'm assuming @ is immediately prefixed by a ' or "
            int lastQuotePosition = jsLine.LastIndexOf(firstQuoteCharaacter);

            string fragment = jsLine.Substring(atPosition - 1, lastQuotePosition - atPosition + 2);

            output.Add(fragment);
            
            return output;
        }
    }
}
