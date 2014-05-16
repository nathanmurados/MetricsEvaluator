using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsModuleBlockEvaluator
    {
        IEnumerable<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines);
    }
}