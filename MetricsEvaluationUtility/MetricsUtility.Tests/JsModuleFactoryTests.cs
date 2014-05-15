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

            };

            var result = JsModuleFactory.Build();
        }
    }

}