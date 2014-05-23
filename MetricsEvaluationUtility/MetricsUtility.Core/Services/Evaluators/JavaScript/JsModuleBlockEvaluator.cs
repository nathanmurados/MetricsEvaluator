using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsModuleBlockEvaluator : IJsModuleBlockEvaluator
    {
        public IJsModuleLineEvaluator JsModuleLineEvaluator { get; private set; }
        public IJsVariableNameEvaluator JsVariableNameEvaluator { get; private set; }
        
        public JsModuleBlockEvaluator(IJsModuleLineEvaluator jsModuleLineEvaluator, IJsVariableNameEvaluator jsVariableNameEvaluator)
        {
            JsVariableNameEvaluator = jsVariableNameEvaluator;
            JsModuleLineEvaluator = jsModuleLineEvaluator;
        }

        /// <summary>
        /// From a block of JS, extract razor fragments and generate a variable name for each.
        /// </summary>
        public List<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines)
        {
            // input:
            // multiple lines of JS, some of which use @razor variable/functions

            // output:
            // a collection of those razor fragments, each with a corresponding variable name (generated from the razor fragment)


            List<JsModuleViewModel> output = new List<JsModuleViewModel>();

            foreach (string jsLine in jsLines)
            {
                if (jsLine.Contains("@@") || !jsLine.Contains("@"))
                {
                    continue;
                }

                IEnumerable<Fragment> razorFragments = JsModuleLineEvaluator.Evaluate(jsLine);

                foreach (Fragment fragment in razorFragments)
                {
                    string razorVariable = JsVariableNameEvaluator.Evaluate(fragment.Text);
                    output.Add(new JsModuleViewModel() { JavaScriptName = razorVariable, OriginalRazorText = fragment.Text });
                }
            }

            return output;
        }
    }
}