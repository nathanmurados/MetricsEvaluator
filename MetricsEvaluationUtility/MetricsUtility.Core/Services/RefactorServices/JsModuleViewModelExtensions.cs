using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public static class JsModuleViewModelExtensions
    {
        public static string GetAp2Name(this JsModuleViewModel vm)
        {
            return string.Format("ap2.{0}", vm.JavaScriptName);
        }
    }
}