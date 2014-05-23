using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsModuleBlockEvaluator2 : IJsModuleBlockEvaluator
    {
        public IJsModuleLineEvaluator JsModuleLineEvaluator { get; private set; }
        public IJsVariableNameEvaluator JsVariableNameEvaluator { get; private set; }

        public JsModuleBlockEvaluator2(IJsModuleLineEvaluator jsModuleLineEvaluator, IJsVariableNameEvaluator jsVariableNameEvaluator)
        {
            JsVariableNameEvaluator = jsVariableNameEvaluator;
            JsModuleLineEvaluator = jsModuleLineEvaluator;
        }

        public List<JsModuleViewModel> Evaluate(IEnumerable<string> jsLines)
        {
            var output = new List<JsModuleViewModel>();

            foreach (var jsLine in jsLines)
            {
                if (jsLine.Contains("@@") || !jsLine.Contains("@"))
                {
                    continue;
                }

                var razorFragments = JsModuleLineEvaluator.Evaluate(jsLine);

                foreach (var fragment in razorFragments)
                {
                    var razorVariable = JsVariableNameEvaluator.Evaluate(fragment.Text);
                    output.Add(new JsModuleViewModel
                    {
                        JavaScriptName = razorVariable, 
                        OriginalRazorText = fragment.Text,
                        FragType = fragment.FragType
                    });
                }
            }

            return output;
        }
    }
}