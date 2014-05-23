using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtiltiy.Tests
{
    public class ProcessorsToTest
    {
        public static IJsModuleLineEvaluator GetJsModuleLineEvaluator()
        {
            return new JsModuleLineEvaluator();
        }

        public static IJsModuleBlockEvaluator GetJsModuleBlockEvaluator()
        {
            return new JsModuleBlockEvaluator(GetJsModuleLineEvaluator(), new JsVariableNameEvaluator());
        }

        public static IJsInjectNewModuleVariables GetJsInjectNewModuleVariables()
        {
            return new JsInjectNewModuleVariables();
        }
    }
}