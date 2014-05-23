using System.Collections.Generic;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsModuleBlockEvaluator2Tests
    {
        [Test]
        public void Test_Single_Line()
        {
            var obj = new JsModuleBlockEvaluator2(new JsModuleLineEvaluator2(), new JsVariableNameEvaluator());

            var data = new[]
            {
                "   $(function(){",
                "       something='@abc'",
                "   });"
            };

            List<JsModuleViewModel> result = obj.Evaluate(data);

            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].OriginalRazorText, "@abc");
            Assert.AreEqual(result[0].JavaScriptName, "abc");
        }

        [Test]
        public void Test_Complex()
        {
            var obj = new JsModuleBlockEvaluator2(new JsModuleLineEvaluator2(), new JsVariableNameEvaluator());

            var data = new[]
            {
                "   $(function(){",
                "       something='@abc'",                    
                "   });",
                "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';",
                "$('#DecommisionReason').val('@decommisionReason');"
            };

            List<JsModuleViewModel> result = obj.Evaluate(data);

            Assert.IsTrue(result.Count == 3);

            Assert.AreEqual(result[0].OriginalRazorText, "@abc");
            Assert.AreEqual(result[0].JavaScriptName, "abc");

            Assert.AreEqual(result[1].OriginalRazorText, "@Url.Action(\"Configure\", \"ConfigureMenu\")");
            Assert.AreEqual(result[1].JavaScriptName, "UrlActionConfigureConfigureMenu");

            Assert.AreEqual(result[2].OriginalRazorText, "@decommisionReason");
            Assert.AreEqual(result[2].JavaScriptName, "decommisionReason");
        }
    }
}