using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JSModuleLineEvaluatorRegex: IJsModuleLineEvaluator
    {
        public IEnumerable<string> Evaluate(string jsLine)
        {
            IList<string> output =new List<string>();
            MatchCollection mc = Regex.Matches(jsLine, @"'(.*?)'");
            foreach (Match m in mc)
            {

            }

            return output;
        }
    }
}
