using System.Collections.Generic;
using MetricsEvaluationUtility.Services.Evaluators;
using MetricsEvaluationUtility.ViewModels;
using NUnit.Framework;

namespace MetricsEvaluationUtiltiyTests
{
    [TestFixture]
    public class RelevantAttributesEvaluatorTests
    {
        [Test]
        public void Evaluate()
        {
            var evaluator = new RelevantAttributesEvaluator();

            var testData = new List<JavaScriptEvaluationResult>
            {
                new JavaScriptEvaluationResult
                {
                    Block = new List<DetailedJavaScriptEvaluationResult>
                    {
                        new DetailedJavaScriptEvaluationResult { AttributeName = "onclick" },
                        new DetailedJavaScriptEvaluationResult { AttributeName = "onblur" },
                    },
                    Razor = new List<DetailedJavaScriptEvaluationResult>
                    {
                        new DetailedJavaScriptEvaluationResult { AttributeName = "onclick" },
                        new DetailedJavaScriptEvaluationResult { AttributeName = "ondblclick" },
                    },
                }
            };

            var result = evaluator.Evaluate(testData);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("onclick", result[0]);
            Assert.AreEqual("onblur", result[1]);
            Assert.AreEqual("ondblclick", result[2]);
        }
    }
}