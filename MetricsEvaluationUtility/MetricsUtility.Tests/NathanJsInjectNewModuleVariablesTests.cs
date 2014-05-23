using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsInjectNewModuleVariablesTestsNathan
    {
        [Test]
        public void Test_Single_Line()
        {
            var obj = new JsInjectNewModuleVariables2();

            var data = new List<string>
            {
                "   $(function(){",
                "       something='@abc'", 
                "   });"
            };

            var razorLines = new List<JsModuleViewModel>
            {
                new JsModuleViewModel
                {
                    OriginalRazorText = "@abc", 
                    JavaScriptName = "abc"
                }
            };

            var result = obj.Build(data, razorLines);
            
            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual("       something=ap2.abc", result[1]);
        }

        [Test]
        public void Test_Multi_Line()
        {
            var obj = new JsInjectNewModuleVariables2();

            var data = new List<string>
            {
                "   $(function(){",
                "       something='@abc'",
                "       var addPageUrl = @Url.Action(\"Configure\", \"ConfigureMenu\");",
                "       $('#DecommisionReason').val('@decommisionReason');",
                "   });"
            };

            var razorLines = new List<JsModuleViewModel>
            {
                new JsModuleViewModel
                {
                    OriginalRazorText = "@abc", 
                    JavaScriptName = "abc"
                },
                new JsModuleViewModel
                {
                    OriginalRazorText = "@Url.Action(\"Configure\", \"ConfigureMenu\")",
                    JavaScriptName = "UrlActionConfigureConfigureMenu"
                },
                new JsModuleViewModel
                {
                    OriginalRazorText = "@decommisionReason",
                    JavaScriptName = "decommisionReason"
                }
            };

            var result = obj.Build(data, razorLines);

            Assert.IsTrue(result.Count == 5);
            Assert.AreEqual("       something=ap2.abc", result[1]);
            Assert.AreEqual("       var addPageUrl = ap2.UrlActionConfigureConfigureMenu;", result[2]);
            Assert.AreEqual("       $('#DecommisionReason').val(ap2.decommisionReason);", result[3]);
        }
    }
}