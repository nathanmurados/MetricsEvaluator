using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IJsModuleFactory
    {
        string[] Build(List<JsModuleViewModel> data);
    }
}