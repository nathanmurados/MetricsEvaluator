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
            if (obj == null )
            {
                return false;
            }
            
            return ((JsModuleViewModel)obj).JavaScriptName == this.JavaScriptName;
        }

        public override int GetHashCode()
        {
            return JavaScriptName.GetHashCode();
        }
    }
}