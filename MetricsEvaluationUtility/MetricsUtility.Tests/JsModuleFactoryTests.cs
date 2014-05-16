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
                new JsModuleViewModel{JavaScriptName = "ap2.ViewDataSubject",OriginalRazorText = "@ViewData[\"Subject\"]"},
                new JsModuleViewModel{JavaScriptName = "ap2.Model0MessageListActivitiesConstantsPleaseEnterValue",OriginalRazorText = "@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]"},
                new JsModuleViewModel{JavaScriptName = "ap2.ViewBag.pageId",OriginalRazorText = "@ViewData[\"Subject\"]"},
            };

            var result = JsModuleFactory.Build();
        }
    }

}