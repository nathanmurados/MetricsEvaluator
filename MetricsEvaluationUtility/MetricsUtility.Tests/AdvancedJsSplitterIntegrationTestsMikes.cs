using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using Moq;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class AdvancedJsSplitterIntegrationTestsMikes
    {
        public AdvancedJsSeperationService GetObj()
        {
            return new AdvancedJsSeperationService(
                new JsBlockContentEvaluator(),
                new JsFileNameEvaluator(
                    new SolutionRelativeDirectoryEvaluator()),
                new JsModuleBlockEvaluator(new JsModuleLineEvaluator(), new JsVariableNameEvaluator()), 
                new JsModuleFactory(),
                new JsInjectNewModuleVariables()
                );
        }

        [Test]
        public void EmbeddedWithinQuotes1()
        {
            var obj = GetObj();

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

        [Test]
        public void EmbeddedWithinQuotes3()
        {
            var obj = GetObj();

            var input = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an ' + \"@Viewmodel.Variable\");",
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
                "        ap2.ViewmodelVariable = \"@Viewmodel.Variable\";", 
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

        [Test]
        public void EmbeddedWithinQuotes4()
        {
            var obj = GetObj();

            var input = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       alert('I am a script with an ' + \"@Viewmodel.Variable\" + ' and another ' + '@Viewmodel.Variable2');",
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
                "        ap2.ViewmodelVariable = \"@Viewmodel.Variable\";", 
                "        ap2.ViewmodelVariable2 = \'@Viewmodel.Variable2\';", 
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

        [Test]
        public void EmbeddedWithinQuotes2()
        {
            var obj = GetObj();

            var input = new[]
            {
                "<html>",
                "<head>",
                "</head>",
                "<body>",
                "<script type='text/javascript'>",
                "   $(function(){",
                "       something='@abc'",                    
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
                "        ap2.abc = '@abc';", 
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
            mockJsInjectNewModuleVariables.Setup(x => x.Build(It.IsAny<List<string>>(), It.IsAny<List<JsModuleViewModel>>())).Returns(() =>
                new List<string>
                {
                    "   $(function(){", 
                    "       var item =  ap2.Variable;",
                    "   });"
                });

            return
                new AdvancedJsSeperationService(
                    new JsBlockContentEvaluator(),
                    new JsFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()),
                    new JsModuleBlockEvaluator(new JsModuleLineEvaluator(), new JsVariableNameEvaluator()), 
                    new JsModuleFactory(),
                    mockJsInjectNewModuleVariables.Object
                    );
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
                "       var item =  @Viewmodel.Variable;", // this is a typical js line with razor
                "   });",
                "</script>",
                "</body>",
                "</html>",
            };

            var result = obj.Evaluate(data, "Z:\\SomeDirectory\\Project", "Z:\\SomeDirectory\\Project\\BlockJs", "somefile.cshtml");

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
                "       var item =  @Viewmodel.Variable;", // this is a typical js line with razor
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

        [Test]
        public void GetComplianceMatrixShouldWork()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.GetComplianceMatrix);

            var obj = GetObj();

            var result = obj.Evaluate(raw.Contents, "Z:\\SomeDirectory\\Project", "Z:\\SomeDirectory\\Project\\BlockJs", "somefile.cshtml");

        }
    }
}