using System.Linq;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Refactorers;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class AdvancedJsSplitterIntegrationTests
    {
        [Test]
        public void BreakUpMixedRazorBlocks()
        {
            var obj = new JsBlockContentEvaluator();

            var data = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script without any at variables);",                    
                "   });",
                "</script>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an @Viewmodel.Variable);",                    
                "   });",
                "</script>",
                "</body>",
                "</html>",
            };
            var result = obj.Evaluate(data, JsPageEvaluationMode.RazorOnly);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(3, result[0].Lines.Count);
        }

        [Test]
        public void EnsureJsPageEvaluatorRespectsMode()
        {
            var obj = new JsBlockContentEvaluator();

            var data = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script without any at variables);",                    
                "   });",
                "</script>",
                "<!-- some text-->",    //TODO: Consider how to treat "joined" blocks.
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an @Viewmodel.Variable);",                    
                "   });",
                "</script>",
                "</body>",
                "</html>",
            };

            var result = obj.Evaluate(data, JsPageEvaluationMode.RazorOnly);

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void IgnoreNonRazorJs()
        {
            var obj = new AdvancedJsSeperationService(new JsBlockContentEvaluator(), new JsFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()), new JsModuleBlockEvaluator(new JsModuleLineEvaluator()), new JsModuleFactory());

            var data = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script without any at variables);",                    
                "   });",
                "</script>",
                "<!-- some text-->",    //TODO: Consider how to treat "joined" blocks.
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an @Viewmodel.Variable);",                    
                "   });",
                "</script>",
                "</body>",
                "</html>",
            };

            var result = obj.Evaluate(data, "Z:\\SomeDirectory\\Project", "Z:\\SomeDirectory\\Project\\BlockJs", "somefile.cshtml");

            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual(3, result.ExtractedJsBlocks[0].Lines.Count);
            Assert.IsNull(result.ExtractedJsBlocks[0].Lines.FirstOrDefault(x => x.Contains("@")));
        }
    }
}