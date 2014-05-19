using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;
using MetricsUtility.Core.Services.Evaluators.JavaScript;

namespace MetricsUtiltiy.Tests
{
    /// <summary>
    /// Testing ability to process a block of javascript that contains @razor code.
    /// This is an integration test as the evaluator unit we are testing makes use of several other evaluator units.
    /// </summary>
    [TestFixture]
    public class JsModuleBlockEvaluatorTests
    {
        [Test]
        public void Test_Single_Line()
        {
            var obj = new JsModuleBlockEvaluator(new JsModuleLineEvaluator());
            
            var data = new[]
            {
                "   $(function(){",
                "       something='@abc'",                    

                "   });"
            };

            List<JsModuleViewModel> result = obj.Evaluate(data);

            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].OriginalRazorText, "'@abc'");
            Assert.AreEqual(result[0].JavaScriptName, "abc");
        }

        [Test]
        public void Test_Complex()
        {
            var obj = new JsModuleBlockEvaluator(new JsModuleLineEvaluator());

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
            
            Assert.AreEqual(result[0].OriginalRazorText, "'@abc'");
            Assert.AreEqual(result[0].JavaScriptName, "abc");

            Assert.AreEqual(result[1].OriginalRazorText, "'@Url.Action(\"Configure\", \"ConfigureMenu\")'");
            Assert.AreEqual(result[1].JavaScriptName, "UrlActionConfigureConfigureMenu");

            Assert.AreEqual(result[2].OriginalRazorText, "'@decommisionReason'");
            Assert.AreEqual(result[2].JavaScriptName, "decommisionReason");
        }
    }
}