using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MetricsEvaluationUtility.Services;
using MetricsEvaluationUtility.Services.Evaluators;
using MetricsEvaluationUtility.Services.Storers;
using MetricsEvaluationUtility.ViewModels;
using Moq;
using NUnit.Framework;

namespace MetricsEvaluationUtiltiyTests
{
    [TestFixture]
    public class CssStorage
    {
        [Test]
        public void StoreJavascript()
        {
            var mockRelevantAttributesEvaluator = new Mock<IRelevantAttributesEvaluator>();
            var mockHumanInterface = new Mock<IHumanInterface>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockStorer = new Mock<IStorer>();

            mockRelevantAttributesEvaluator.Setup(x => x.Evaluate(It.IsAny<List<JavaScriptEvaluationResult>>())).Returns(() => new List<string> { "onclick", "onblur", "ondblclick" });

            mockStorer.Setup(x => x.Store(It.IsAny<StringBuilder>(), It.IsAny<string>())).Callback<StringBuilder, string>((sb, title) =>
            {
                var str = sb.ToString();

                Assert.AreEqual(21, str.Count(x => x == ','));
                Assert.AreEqual(2, Regex.Matches(str, Environment.NewLine).Count);
            });

            var evaluator = new JavaScriptStatsStorer(mockStorer.Object, mockDateTimeProvider.Object, mockHumanInterface.Object, mockRelevantAttributesEvaluator.Object);

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
                    PageInstances = new List<int> { 1,0 }, 
                    FileName = "test",
                    References = 1
                }
            };

            evaluator.Store(testData);
        }

    }
}