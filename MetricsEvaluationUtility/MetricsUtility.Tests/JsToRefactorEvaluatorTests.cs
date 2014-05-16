﻿using System;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;

    [TestFixture]
    public class JsToRefactorEvaluatorTests
    {
        [Test]
        public void Extract_Razor_1()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "var selectedMenu = '@ViewBag.MenuInstanceName';";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("'@ViewBag.MenuInstanceName'", result[0]);
        }

        [Test]
        public void Extract_Razor_2()
        {
            throw new NotImplementedException("Would who ever works on this please discuss this with Nathan");

            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Url.Action(\"Configure\", \"ConfigureMenu\")", result[0]);
        }
        [Test]
        public void Extract_Razor_3()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "$('#DecommisionReason').val('@decommisionReason');";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("'@decommisionReason'", result);
        }
        [Test]
        public void Extract_Razor_4()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("@Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList))", result[0]);
        }
        [Test]
        public void Extract_Razor_5()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();

            // NB contains 2 fragments of razor
            string input = " data: \"{'docId1':'\" + '@ViewBag.docid' + \"','conditionType1':'\" + '@ViewBag.doctype' + \"'}\",";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("'@ViewBag.docid'", result[0]);
            Assert.AreEqual("'@ViewBag.doctype'", result[1]);
        }
        [Test]
        public void Extract_Razor_6()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "$('#HiddenName').val('@Convert.ToString(stateWatcherVM.LName)');";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("@Convert.ToString(stateWatcherVM.LName)'", result[0]);
        }

    }
}
