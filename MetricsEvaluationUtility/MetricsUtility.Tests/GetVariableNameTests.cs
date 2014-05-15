using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;

    [TestFixture]
    public class GetVariableNameTests
    {
        [Test]
        public void Extract_Varable_Name_1()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@ViewData[\"Subject\"]";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ViewDataSubject", result);
        }
    }
}
