using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class JsModuleFactory : IJsModuleFactory
    {
        private const string JsContainerName = "ap2";

        /// <summary>
        /// Takes a list of razor fragments and corresponding variables names and turns into a JS module.
        /// </summary>
        public string[] Build(IEnumerable<JsModuleViewModel> data)
        {
            // input:
            //      supplierList    '@supplierList'
            //      elements        '@elements'
            //      SomeInt          @Model.SomeInt
            //
            // output:
            //<script type="text/javascript">
            //    var ap2 = (function (ap2) {
            //        ap2.supplierList = '@supplierList';
            //        ap2.elements = '@elements';
            //        ap2.SomeInt = @Model.SomeInt;
            //        return ap2;
            //    }(ap2 || {}));
            //<\script>

            if (data == null || !data.Any())
            {
                throw new ArgumentException("Null or empty arguments.");
            }

            int i = 0;
            int moduleLines = data.Count() + 5;
            const string indent1 = "    ";     // may change to tabs
            const string indent2 = "        "; // may change to tabs
            string[] result = new string[moduleLines];

            result[i++] = "<script type=\"text/javascript\">";
            result[i++] = String.Format("{0}var {1} = (function({1}) {{", indent1, JsContainerName);

            foreach (JsModuleViewModel item in data)
            {
                var surroundingQuote = "";
                if (item.FragType == FragType.SingleQuotes)
                {
                    surroundingQuote = "\'";
                }
                else if (item.FragType == FragType.DoubleQuotes)
                {
                    surroundingQuote = "\"";
                }

                result[i++] = string.Format("{0}{1}.{2} = {4}{3}{4};", indent2, JsContainerName, item.JavaScriptName, item.OriginalRazorText, surroundingQuote);
            }

            result[i++] = String.Format("{0}return {1};", indent2, JsContainerName);
            result[i++] = String.Format("{0}}} ({1} || {{}}));", indent1, JsContainerName);  //TODO: Discuss with Nathan
            result[i++] = "</script>";

            return result;
        }
    }
}