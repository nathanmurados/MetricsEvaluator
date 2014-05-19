using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class JsInjectNewModuleVariables : IJsInjectNewModuleVariables
    {
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

            foreach (string line in lines)
            {
                foreach (JsModuleViewModel razorVariable in razorVariables)
                {
                    
                }
            }

            return null;
        }
    }
}