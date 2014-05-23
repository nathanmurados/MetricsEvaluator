using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtiltiy.Tests
{
    public class ProcessorsToTest
    {
        public static IJsModuleBlockEvaluator GetJsModuleBlockEvaluator()
        {
            return new JsModuleBlockEvaluator2(new JsModuleLineEvaluator2(), new JsVariableNameEvaluator());
        }

        public static IJsInjectNewModuleVariables GetJsInjectNewModuleVariables()
        {
            return new JsInjectNewModuleVariables2();
        }
    }
}