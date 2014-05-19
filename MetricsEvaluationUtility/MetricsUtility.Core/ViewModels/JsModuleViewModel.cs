namespace MetricsUtility.Core.ViewModels
{
    public class JsModuleViewModel
    {
        public string OriginalRazorText { get; set; }
        public string JavaScriptName { get; set; }

        /// <summary>
        /// Override so that when dealing with a list of these objects, equality is based on a value rather than the reference to the objects.
        /// </summary>
        public override bool Equals(object obj)
        {
            JsModuleViewModel jsModuleViewModel = obj as JsModuleViewModel;
            
            if (obj == null )
            {
                return false;
            }

            return jsModuleViewModel.JavaScriptName == this.JavaScriptName;
        }

        /// <summary>
        /// Normally overriding equals is sufficient, but Linq.Distinct seems to require this too.
        /// </summary>
        public override int GetHashCode()
        {
            return JavaScriptName.GetHashCode();
        }
    }
}