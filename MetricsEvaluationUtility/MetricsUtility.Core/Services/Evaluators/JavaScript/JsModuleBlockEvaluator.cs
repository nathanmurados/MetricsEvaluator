using System;
using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsModuleBlockEvaluator : IJsModuleBlockEvaluator
    {
        public IJsModuleLineEvaluator JsModuleLineEvaluator { get; private set; }
        public JsModuleBlockEvaluator(IJsModuleLineEvaluator jsModuleLineEvaluator)
        {
            JsModuleLineEvaluator = jsModuleLineEvaluator;
        }


        public IEnumerable<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines)
        {
            throw new NotImplementedException();
        }
    }
}