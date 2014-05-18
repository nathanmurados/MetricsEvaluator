using System;
using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    using System.Text;

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
            // input:
            // multiple lines of JS, some of which use @razor variable/functions
            // NB there might be lines containing @@ to ignore, although the same line might also contain a @ to process, but unlikely.

            // output:
            // a collection of those razor fragments, each with a corresponding variable name (generated from the razor fragment)

            List<JsModuleViewModel> output = new List<JsModuleViewModel>();

            JsModuleLineEvaluator lineEvaluator = new JsModuleLineEvaluator();
            JsVariableNameEvaluator variableNameEvaluator = new JsVariableNameEvaluator();

            foreach (string jsLine in jsLines)
            {
                if (jsLine.Contains("@@") || !jsLine.Contains("@"))
                {
                    continue;
                }

                IEnumerable<string> razorFragments = lineEvaluator.Evaluate(jsLine);

                foreach (string fragment in razorFragments)
                {
                    string razorVariable = variableNameEvaluator.Evaluate(fragment);
                    output.Add(new JsModuleViewModel() { JavaScriptName = razorVariable, OriginalRazorText = fragment });
                }
            }

            return output;
        }
    }
}