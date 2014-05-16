using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    
    public class JsVariableNameEvaluator : IJsVariableNameEvaluator
    {
        private static char[] charactersToRemove = new char[] { '@', '"', '[', ']', '(', ')', ' ', ';', ',', '.'};
        /// <summary>
        /// Extract a variable name from the razor code.
        /// Example input: @ViewData["Subject"]"
        /// Note that the input string is not surrounded by quotes
        /// </summary>
        /// <param name="razorCode"></param>
        /// <returns></returns>
        public string Evaluate(string razorCode)
        {
            return new string(razorCode.Where(c => !JsVariableNameEvaluator.charactersToRemove.Contains(c)).ToArray());
        }
    }
}
