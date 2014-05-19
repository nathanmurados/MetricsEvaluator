using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsBlockContentEvaluatorTests
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
            Assert.AreEqual(5, result[0].Lines.Count);
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
                "<!-- some text-->", //TODO: Consider how to treat "joined" blocks.
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
        
    }
}