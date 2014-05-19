using System.Collections.Generic;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtiltiy.Tests
{
    using System.Text;

    /// <summary>
    /// Testing ability to replace razor fragments in JS blocks with ap2 variable names
    /// </summary>
    [TestFixture]
    public class JsInjectNewModuleVariablesTests
    {
        [Test]
        public void Test_Single_Line()
        {
            // Arrange
            var obj = new JsInjectNewModuleVariables();

            List<string> data = new List<string>();
            data.Add("   $(function(){");
            data.Add("       something='@abc'");
            data.Add("   });");

            List<JsModuleViewModel> razorLines = new List<JsModuleViewModel>();
            razorLines.Add(new JsModuleViewModel(){OriginalRazorText = "'@abc'", JavaScriptName = "abc"});

            // Act
            List<string> result = obj.Build(data, razorLines);

            // Assert
            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual(result[1], "       something=ap2.abc");
        }

        [Test]
        public void Test_Multi_Line()
        {
            // Arrange
            var obj = new JsInjectNewModuleVariables();

            List<string> data = new List<string>();
            data.Add("   $(function(){");
            data.Add("       something='@abc'");
            data.Add("       var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';");
            data.Add("       $('#DecommisionReason').val('@decommisionReason');");
            data.Add("   });");


            List<JsModuleViewModel> razorLines = new List<JsModuleViewModel>();
            razorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@abc'", JavaScriptName = "abc" });
            razorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@Url.Action(\"Configure\", \"ConfigureMenu\")'", JavaScriptName = "UrlActionConfigureConfigureMenu" });
            razorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@decommisionReason'", JavaScriptName = "decommisionReason" });

            // Act
            List<string> result = obj.Build(data, razorLines);

            // Assert
            Assert.IsTrue(result.Count == 5);
            Assert.AreEqual("       something=ap2.abc", result[1]);
            Assert.AreEqual("       var addPageUrl = ap2.UrlActionConfigureConfigureMenu;", result[2]);
            Assert.AreEqual("       $('#DecommisionReason').val(ap2.decommisionReason);", result[3]);

        }
    }
}