using System.Linq;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;
using NUnit.Framework;
using System.Linq;
using MetricsUtility.Core.ViewModels;
using System.Collections.Generic;

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

        [Test]
        public void DeDuplicationTest()
        {
            // Just a quick test of the de-duplication of a list of objects based on an object property value.
            // See JsModuleViewModel.Equals() override.
            // Without the override the items below wouldn't be considered duplicates because by default the equality is based on reference to objects.

            // Arrange
            List<JsModuleViewModel> totalRazorLines = new List<JsModuleViewModel>();
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable1'", JavaScriptName = "serverVariable1" });
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable1'", JavaScriptName = "serverVariable1" }); // Duplicate
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" });
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable3'", JavaScriptName = "serverVariable3" });
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" }); // Duplicate

            // Act
            totalRazorLines = totalRazorLines.Distinct().ToList();

            // Assert
            // To illustrate, without the equals override, this wouldn't work because although the values match they are difference objects.
            Assert.IsTrue(totalRazorLines.Contains(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" }));

            Assert.AreEqual(3, totalRazorLines.Count);
            Assert.AreEqual(totalRazorLines[0].JavaScriptName, "serverVariable1");
            Assert.AreEqual(totalRazorLines[1].JavaScriptName, "serverVariable2");
            Assert.AreEqual(totalRazorLines[2].JavaScriptName, "serverVariable3");
        }
    }
}