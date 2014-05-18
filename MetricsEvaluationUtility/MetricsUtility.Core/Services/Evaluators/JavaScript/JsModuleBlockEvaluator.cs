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


        /// <summary>
        /// From a block of JS, extract razor fragments and generate a variable name for each
        /// </summary>
        public IEnumerable<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines)
        {
            throw new NotImplementedException();

            // input:
            // multiple lines of JS, some of which use @razor variable/functions

            // output:
            // a collection of those razor fragments, each with a corresponding variable name (generated from the razor fragment)


        }
    }
}