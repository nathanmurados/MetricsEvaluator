using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class JsInjectNewModuleVariables : IJsInjectNewModuleVariables
    {
        private const string JsContainerName = "ap2";

        /// <summary>
        /// Take a JS block and replace razor fragments with ap2 variables
        /// </summary>
        public List<string> Build(List<string> lines, IEnumerable<JsModuleViewModel> razorVariables)
        {
            // input:
            //      razorVariable    '@razorVariable'
            //
             // input:
            //  "<script type='text/javascript'>",
            //    "   $(function(){",
            //    "       alert('@razorVariable');",                    
            //    "   });",
            //    "</script>",
            //
            // output:
            //  "<script type='text/javascript'>",
            //    "   $(function(){",
            //    "       alert(ap2.razorVariable);",                    
            //    "   });",
            //    "</script>",

            var output = new List<string>();
            
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                
                foreach (JsModuleViewModel razor in razorVariables)
                {
                    if (line.Contains(razor.OriginalRazorText))
                    {
                        line = line.Replace(razor.OriginalRazorText, string.Format("{0}.{1}", JsContainerName, razor.JavaScriptName));
                    }
                }

                output.Add(line);
            }

            return output;
        }
    }
}