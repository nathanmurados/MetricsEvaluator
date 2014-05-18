using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsModuleBlockEvaluator
    {
        /// <summary>
        /// From a block of JS, extract razor fragments and generate a variable name for each
        /// </summary>
        List<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines);
    }
}