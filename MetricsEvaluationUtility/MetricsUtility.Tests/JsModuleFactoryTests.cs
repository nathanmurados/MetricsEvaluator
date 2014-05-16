using System.Collections.Generic;
using MetricsUtility.Core.Services.Refactorers;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsModuleFactoryTests
    {
        public void Test1()
        {
            var obj = new JsModuleFactory();

            var data = new List<JsModuleViewModel>
            {
                new JsModuleViewModel{ JavaScriptName = "ap2.ViewDataSubject", OriginalRazorText = "'@ViewData[\"Subject\"]'" },
                new JsModuleViewModel{ JavaScriptName = "ap2.Model0MessageListActivitiesConstantsPleaseEnterValue", OriginalRazorText = "'@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]'" },
                new JsModuleViewModel{ JavaScriptName = "ap2.ModelMessageListMSG1005884", OriginalRazorText = "'@Model.MessageList[\"MSG1005884\"]'"},
                new JsModuleViewModel{ JavaScriptName = "ap2.SomeInt", OriginalRazorText = "@Model.SomeInt"},
            };

            var result = JsModuleFactory.Build(data);

            Assert.AreEqual("var ap2 = (function(ap2) {", result[0]);
            Assert.AreEqual("   ap2.ViewDataSubject = '@ViewData[\"Subject\"]';", result[0]);
            Assert.AreEqual("   ap2.Model0MessageListActivitiesConstantsPleaseEnterValue = '@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]';", result[0]);
            Assert.AreEqual("   ap2.ModelMessageListMSG1005884 = '@Model.MessageList[\"MSG1005884\"]';", result[0]);
            Assert.AreEqual("   ap2.SomeInt = @Model.SomeInt;", result[0]);
            Assert.AreEqual("   return ap2;", result[0]);
            Assert.AreEqual("} (ap2 || {}));", result[0]);

        }
    }

}