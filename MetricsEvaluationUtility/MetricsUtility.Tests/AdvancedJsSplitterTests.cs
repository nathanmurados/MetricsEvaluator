﻿using System.Diagnostics;
using System.Linq;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;
using Moq;
using NUnit.Framework;
using MetricsUtility.Core.ViewModels;
using System.Collections.Generic;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class AdvancedJsSplitterIntegrationTests
    {
        [Test]
        public void IntegrationTest1()
        {
            var obj = new AdvancedJsSeperationService(
                new JsBlockContentEvaluator(),
                new JsFileNameEvaluator(
                    new SolutionRelativeDirectoryEvaluator()),
                new JsModuleBlockEvaluator(
                    new JsModuleLineEvaluator()
                ),
                new JsModuleFactory(),
                new JsInjectNewModuleVariables()
                );

            var input = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an ' + '@Viewmodel.Variable');",
                "   });",
                "</script>",
                "   <div>",
                "       test",
                "   </div>",
                "</body>",
                "</html>"
            };

            var expected = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type=\"text/javascript\">",
                "    var ap2 = (function(ap2) {", 
                "        ap2.ViewmodelVariable = '@Viewmodel.Variable';", 
                "        return ap2;", 
                "    } (ap2 || {}));", 
                "</script>", 
                "<script src=\"~/blockjs/somefile.js\" type=\"text/javascript\"></script>",
                "   <div>",
                "       test",
                "   </div>",
                "</body>",
                "</html>"
            };

            var result = obj.Evaluate(input, "c:\\code", "c:\\code\\blockjs", "somefile.cshtml");

            for (var i = 0; i < result.RefactoredLines.Length; i++)
            {
                Debug.WriteLine(expected[i]);
                Debug.WriteLine(result.RefactoredLines[i]);
                Debug.WriteLine("");
                Assert.AreEqual(expected[i], result.RefactoredLines[i]);
            }
        }

        private AdvancedJsSeperationService GetAdvancedJsSeperationService()
        {
            var mockJsInjectNewModuleVariables = new Mock<IJsInjectNewModuleVariables>();
            mockJsInjectNewModuleVariables.Setup(x => x.Build(It.IsAny<List<string>>(), It.IsAny<IEnumerable<JsModuleViewModel>>())).Returns(() =>
                new List<string>
                {
                    "   $(function(){", 
                    "       alert('I am a script with an ' + ap2.ViewmodelVariable);", 
                    "   });"
                });

            return new AdvancedJsSeperationService(new JsBlockContentEvaluator(), new JsFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()), new JsModuleBlockEvaluator(new JsModuleLineEvaluator()), new JsModuleFactory(), mockJsInjectNewModuleVariables.Object);
        }

        [Test]
        public void IgnoreNonRazorJs()
        {
            var obj = GetAdvancedJsSeperationService();

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

            var result = obj.Evaluate(data, "Z:\\SomeDirectory\\Project", "Z:\\SomeDirectory\\Project\\BlockJs",
                "somefile.cshtml");

            Assert.AreEqual(1, result.JsRemoved.Count());
            Assert.AreEqual(3, result.JsRemoved[0].Lines.Count);
            Assert.IsNull(result.JsRemoved[0].Lines.FirstOrDefault(x => x.Contains("@")));
        }

        [Test]
        public void CorrectNumberOfScriptReferences()
        {
            var obj = GetAdvancedJsSeperationService();

            var data = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an @Viewmodel.Variable);",
                "   });",
                "</script>",
                "</body>",
                "</html>",
            };

            var result = obj.Evaluate(data, "Z:\\SomeDirectory\\Project", "Z:\\SomeDirectory\\Project\\BlockJs",
                "somefile.cshtml");

            Assert.AreEqual(1, result.JsRemoved.Count());
            Assert.AreEqual(3, result.JsRemoved[0].Lines.Count);
            Assert.IsNull(result.JsRemoved[0].Lines.FirstOrDefault(x => x.Contains("@")));
            Assert.AreEqual(3, result.RefactoredLines.Count(x => x.Contains("script")));
        }

    }
}