using System.Collections.Generic;
namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    using ViewModels;

    public interface IJsModuleLineEvaluator
    {
        /// <summary>
        /// Extract the razor code from a line of javascript.
        /// </summary>
        List<Fragment> Evaluate(string jsLine);
    }
}
