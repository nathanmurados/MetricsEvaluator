using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Refactorers
{
    using System;

    public class JsModuleFactory : IJsModuleFactory
    {
        private const string JsContainerName = "ap2";

        /// <summary>
        /// Takes a list of razor fragments and corresponding variables names and turns into a JS module.
        /// </summary>
        public string[] Build(List<JsModuleViewModel> data)
        {
            // Currently the razor fragment is surrounded by quotes, but that may not always be the case...
            //
            // input:
            //      supplierList    '@supplierList'
            //      elements        '@elements'
            //
            // output:
            //    var ap2 = (function (ap2) {
            //        ap2.supplierList = '@supplierList';
            //        ap2.elements = '@elements';
            //        return ap2;
            //    }(ap2 || {}));

            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Null or empty arguments.");
            }

            int i = 0;
            int moduleLines = data.Count + 3;
            const string indent1 = "    "; // may change to tabs
            const string indent2 = "        "; // may change to tabs
            string[] result = new string[moduleLines];

            result[i++] = String.Format("{0}var {1} = (function({1}) {{",indent1, JsContainerName);

            foreach (JsModuleViewModel item in data)
            {
                result[i++] = string.Format("{0}{1}.{2} = {3};", indent2, JsContainerName ,item.JavaScriptName, item.OriginalRazorText);
            }

            result[i++] = String.Format("{0}return {1};", indent2, JsContainerName);
            result[i++] = String.Format("{0}}} ({1} || {{}}));",indent1, JsContainerName);  //TODO: Discuss with Nathan

            return result;
        }
    }
}