using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JavaScriptValidationTests
    {
        [Test]
        public void Page()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new JsBlockContentEvaluator(new RemediatedBlockJsRemover());

            var result = evaluator.Evaluate(raw.Contents, PageEvaluationMode.Any, true);

            Assert.AreEqual(2, result.Length);
        }


        [Test]
        public void Block()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new JsBlockEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents), JsAttributesProvider.Attributes);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(2, result[0].InlineJavascriptTags.Count);

            Assert.AreEqual("onclick", result[0].AttributeName);
        }


        [Test]
        public void Razor()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new JsRazorEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents), JsAttributesProvider.Attributes);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].InlineJavascriptTags.Count);

            Assert.AreEqual("onclick", result[0].AttributeName);
        }
    }
}