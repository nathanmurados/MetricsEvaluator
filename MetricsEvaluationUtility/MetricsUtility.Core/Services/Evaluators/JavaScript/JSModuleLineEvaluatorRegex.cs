using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    using ViewModels;

    public class JsModuleLineEvaluatorRegex: IJsModuleLineEvaluator
    {
        public List<Fragment> Evaluate(string jsLine)
        {
            IList<string> output =new List<string>();
            MatchCollection mc = Regex.Matches(jsLine, @"'(.*?)'");
            foreach (Match m in mc)
            {

            }

            //return output.ToList();
            return null;
        }
    }
}
