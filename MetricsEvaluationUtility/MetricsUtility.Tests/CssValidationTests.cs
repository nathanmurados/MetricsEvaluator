using MetricsUtility.Core.Services.Evaluators.Css;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class CssValidationTests : BaseTests
    {
        [Test]
        public void Page()
        {
            var raw = GetFileAndContent();

            var evaluator = new CssPageEvaluator();

            var result = evaluator.Evaluate(raw.Contents);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(6, result[0].Count);
        }


        [Test]
        public void Block()
        {
            var raw = GetFileAndContent();

            var evaluator = new CssBlockEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents));

            Assert.AreEqual(22, result.Count);

            Assert.AreEqual("<td style=\"width: 140px\" align=\"left\">", result[0].Value);
        }

        [Test]
        public void Razor()
        {
            var raw = GetFileAndContent();

            var evaluator = new CssRazorEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents));

            Assert.AreEqual(18, result.Count);

            Assert.AreEqual("{ id = \"ID\", style = \"width:50%\", @readonly = \"readonly\", onclick = \"test\" }", result[0].Value);
        }
    }
}