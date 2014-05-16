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
        public void EnsureJsPageEvaluatorRespectsMode()
        {
            var obj = new JsPageEvaluator();

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

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void IgnoreNonRazorJs()
        {
            var obj = new AdvancedPageJsSeperationEvaluator(new JsPageEvaluator(), new JsFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()));

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