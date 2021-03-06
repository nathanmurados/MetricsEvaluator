﻿using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class CssValidationTests 
    {
        [Test]
        public void Page()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new CssBlockContentEvaluator();

            var result = evaluator.Split(raw.Contents, PageEvaluationMode.Any, false);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(6, result[0].Lines.Count);
        }


        [Test]
        public void Block()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new CssBlockEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents));

            Assert.AreEqual(22, result.Count);

            Assert.AreEqual("<td style=\"width: 140px\" align=\"left\">", result[0].Value);
        }

        [Test]
        public void Razor()
        {
            var raw = AssetRetriever.GetFileAndContent(AvailableTestingResources.TestingResource);

            var evaluator = new CssRazorEvaluator();

            var result = evaluator.Evaluate(string.Join("", raw.Contents));

            Assert.AreEqual(18, result.Count);

            Assert.AreEqual("{ id = \"ID\", style = \"width:50%\", @readonly = \"readonly\", onclick = \"test\" }", result[0].Value);
        }
    }
}