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
    public class JsModuleBlockEvaluatorTests : BaseTests
    {
        [Test]
        public void Test1()
        {
            var obj = new JsModuleBlockEvaluator(new JsModuleLineEvaluator());
            
            var data = new[]
            {
                "   $(function(){",
                "       something='@abc'",                    
                "   });"
            };

            List<JsModuleViewModel> result = obj.Evaluate(data);

            Assert.AreEqual(result[0].OriginalRazorText, "'@abc'");
           
            Assert.AreEqual(result[0].JavaScriptName, "abc");
            
        }
        
    }
}