using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;
using MetricsUtility.Core.Services.Evaluators.Css;

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
                foreach (JsModuleViewModel razor in razorVariables)
                {
                    string lineToProcess = lines[i];

                    if (lineToProcess.Contains(razor.OriginalRazorText))
                    {
                        output.Add(lineToProcess.Replace(razor.OriginalRazorText, string.Format("{0}.{1}", JsContainerName, razor.JavaScriptName)));
                    }
                }
            }

            return lines;
        }
    }
}