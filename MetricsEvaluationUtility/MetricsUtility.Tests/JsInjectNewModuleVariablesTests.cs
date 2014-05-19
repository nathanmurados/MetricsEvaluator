using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtiltiy.Tests
{
    using System.Text;

    /// <summary>
    /// Testing ability to replace razor fragments in JS blocks with ap2 vairable names
    /// </summary>
    [TestFixture]
    public class JsInjectNewModuleVariablesTests
    {
        [Test]
        public void Test_Single_Line()
        {
            //var obj = new JsInjectNewModuleVariables();
            
            //var data = new[]
            //{
            //    "   $(function(){",
            //    "       something='@abc'",                    
            //    "   });"
            //};

            //List<JsModuleViewModel> blockRazorLines = new List<JsModuleViewModel>();

            //List<JsModuleViewModel> result = obj.Build(data, null);

            //Assert.IsTrue(result.Count == 1);
            //Assert.AreEqual(result[0].OriginalRazorText, "'@abc'");
            //Assert.AreEqual(result[0].JavaScriptName, "abc");
        }

        [Test]
        public void Test_Complex()
        {
            //var obj = new JsInjectNewModuleVariables();

            //var data = new[]
            //{
            //    "   $(function(){",
            //    "       something='@abc'",                    
            //    "   });",
            //    "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';",
            //    "$('#DecommisionReason').val('@decommisionReason');"
            //};

            //List<JsModuleViewModel> result = obj.Build(data, null);

            //Assert.IsTrue(result.Count == 3);
            
            //Assert.AreEqual(result[0].OriginalRazorText, "'@abc'");
            //Assert.AreEqual(result[0].JavaScriptName, "abc");

            //Assert.AreEqual(result[1].OriginalRazorText, "'@Url.Action(\"Configure\", \"ConfigureMenu\")'");
            //Assert.AreEqual(result[1].JavaScriptName, "UrlActionConfigureConfigureMenu");

            //Assert.AreEqual(result[2].OriginalRazorText, "'@decommisionReason'");
            //Assert.AreEqual(result[2].JavaScriptName, "decommisionReason");
        }
    }
}