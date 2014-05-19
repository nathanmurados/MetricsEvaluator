using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IJsModuleFactory
    {
        /// <summary>
        /// Takes a list of razor fragments and corresponding variables names and turns into a JS module.
        /// </summary>
        string[] Build(IEnumerable<JsModuleViewModel> data);
    }
}