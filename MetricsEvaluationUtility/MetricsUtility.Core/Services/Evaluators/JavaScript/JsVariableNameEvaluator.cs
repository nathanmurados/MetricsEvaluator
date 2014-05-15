using System;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsVariableNameEvaluator : IJsVariableNameEvaluator
    {
        /// <summary>
        /// Extract a variable name from the razor code.
        /// Example input: @ViewData["Subject"]"
        /// Note that the input string is not surrounded by quotes
        /// </summary>
        /// <param name="razorCode"></param>
        /// <returns></returns>
        public string Evaluate(string razorCode)
        {
            throw new NotImplementedException();
        }
    }
}
