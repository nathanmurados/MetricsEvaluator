using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
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

            Assert.AreEqual("    var ap2 = (function(ap2) {", result[0]);
            Assert.AreEqual("        ap2.ViewDataSubject = '@ViewData[\"Subject\"]';", result[1]);
            Assert.AreEqual("        ap2.Model0MessageListActivitiesConstantsPleaseEnterValue = '@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]';", result[2]);
            Assert.AreEqual("        ap2.ModelMessageListMSG1005884 = \"@Model.MessageList[\"MSG1005884\"]\";", result[3]);
            Assert.AreEqual("        ap2.SomeInt = @Model.SomeInt;", result[4]);
            Assert.AreEqual("        return ap2;", result[5]);
            Assert.AreEqual("    } (ap2 || {}));", result[6]);

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

            Assert.AreEqual("    var ap2 = (function(ap2) {", result[0]);
            Assert.AreEqual("        ap2.ViewBagMenuInstanceName = '@ViewBag.MenuInstanceName';", result[1]);
            Assert.AreEqual("        ap2.UrlActionConfigureConfigureMenu = '@Url.Action(\"Configure\", \"ConfigureMenu\")';", result[2]);
            Assert.AreEqual("        ap2.condition = '@condition';", result[3]);
            Assert.AreEqual("        return ap2;", result[4]);
            Assert.AreEqual("    } (ap2 || {}));", result[5]);

        }
    }
}