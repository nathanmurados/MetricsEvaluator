using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtiltiy.Tests
{
    public class ProcessorsToTest
    {
        public static IJsModuleLineEvaluator GetJsModuleLineEvaluator()
        {
            return new JsModuleLineEvaluator2();
        }

        public static IJsModuleBlockEvaluator GetJsModuleBlockEvaluator()
        {
            return new JsModuleBlockEvaluator2(GetJsModuleLineEvaluator(), new JsVariableNameEvaluator());
        }

        public static IJsInjectNewModuleVariables GetJsInjectNewModuleVariables()
        {
            return new JsInjectNewModuleVariables2();
        }
    }
}