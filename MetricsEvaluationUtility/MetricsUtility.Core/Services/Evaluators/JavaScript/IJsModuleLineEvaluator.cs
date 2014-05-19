using System.Collections.Generic;
namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsModuleLineEvaluator
    {
        /// <summary>
        /// Extract the razor code from a line of javascript.
        /// </summary>
        List<string> Evaluate(string jsLine);
    }
}
