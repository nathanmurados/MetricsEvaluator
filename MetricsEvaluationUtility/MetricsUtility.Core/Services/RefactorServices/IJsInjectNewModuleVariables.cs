using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IJsInjectNewModuleVariables
    {
        /// <summary>
        /// Takes a JS block and replaces razor fragments with ap2 variables
        /// </summary>
        List<string> Build(List<string> lines,List<JsModuleViewModel> razorVariables);
    }
}