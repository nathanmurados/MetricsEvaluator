using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    /// <summary>
    /// Testing generation of the new ap2 module that holds the extracted razor variables
    /// </summary>
    [TestFixture]
    public class JsModuleFactoryTests
    {
        [Test]
        public void Test1()
        {
            var obj = new JsModuleFactory();

            var data = new[]
            {
                new JsModuleViewModel{ JavaScriptName = "ViewDataSubject", OriginalRazorText = "'@ViewData[\"Subject\"]'" },
                new JsModuleViewModel{ JavaScriptName = "Model0MessageListActivitiesConstantsPleaseEnterValue", OriginalRazorText = "'@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]'" },
                new JsModuleViewModel{ JavaScriptName = "ModelMessageListMSG1005884", OriginalRazorText = "\"@Model.MessageList[\"MSG1005884\"]\""},
                new JsModuleViewModel{ JavaScriptName = "SomeInt", OriginalRazorText = "@Model.SomeInt"},
            };

            var result = obj.Build(data);

            int i = 0;
            Assert.AreEqual("<script type=\"text/javascript\">", result[i++]);
            Assert.AreEqual("    var ap2 = (function(ap2) {", result[i++]);
            Assert.AreEqual("        ap2.ViewDataSubject = '@ViewData[\"Subject\"]';", result[i++]);
            Assert.AreEqual("        ap2.Model0MessageListActivitiesConstantsPleaseEnterValue = '@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]';", result[i++]);
            Assert.AreEqual("        ap2.ModelMessageListMSG1005884 = \"@Model.MessageList[\"MSG1005884\"]\";", result[i++]);
            Assert.AreEqual("        ap2.SomeInt = @Model.SomeInt;", result[i++]);
            Assert.AreEqual("        return ap2;", result[i++]);
            Assert.AreEqual("    } (ap2 || {}));", result[i++]);
            Assert.AreEqual("</script>", result[i++]);
        }

        [Test]
        public void Test2()
        {
            var obj = new JsModuleFactory();

            var data = new List<JsModuleViewModel>
            {
                new JsModuleViewModel{ JavaScriptName = "ViewBagMenuInstanceName", OriginalRazorText = "'@ViewBag.MenuInstanceName'" },
                new JsModuleViewModel{ JavaScriptName = "UrlActionConfigureConfigureMenu", OriginalRazorText = "'@Url.Action(\"Configure\", \"ConfigureMenu\")'" },
                new JsModuleViewModel{ JavaScriptName = "condition", OriginalRazorText = "'@condition'"},
            };

            var result = obj.Build(data);

            int i = 0;
            Assert.AreEqual("<script type=\"text/javascript\">", result[i++]);
            Assert.AreEqual("    var ap2 = (function(ap2) {", result[i++]);
            Assert.AreEqual("        ap2.ViewBagMenuInstanceName = '@ViewBag.MenuInstanceName';", result[i++]);
            Assert.AreEqual("        ap2.UrlActionConfigureConfigureMenu = '@Url.Action(\"Configure\", \"ConfigureMenu\")';", result[i++]);
            Assert.AreEqual("        ap2.condition = '@condition';", result[i++]);
            Assert.AreEqual("        return ap2;", result[i++]);
            Assert.AreEqual("    } (ap2 || {}));", result[i++]);
            Assert.AreEqual("</script>", result[i++]);
        }
    }
}