using System.Collections.Generic;
namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsModuleLineEvaluator
    {
        List<string> Evaluate(string jsLine);
    }
}
