using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;

    [TestFixture]
    public class JsVariableNameEvaluatorTests
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

        [Test]
        public void Extract_Varable_Name_1_quoted_razor()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "'@ViewData[\"Subject\"]'";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ViewDataSubject", result);
        }

        [Test]
        public void Extract_Varable_Name_2()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Model[0].MessageList[ActivitiesConstants.PleaseEnterValue]";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("Model0MessageListActivitiesConstantsPleaseEnterValue", result);
        }

        [Test]
        public void Extract_Varable_Name_3()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@ViewBag.pageId";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ViewBagpageId", result);
        }

        [Test]
        public void Extract_Varable_Name_4()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Model.SettingsFacts[ActivitiesConstants.PhoneCallNotes]";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ModelSettingsFactsActivitiesConstantsPhoneCallNotes", result);
        }

        [Test]
        public void Extract_Varable_Name_5()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@errorMessage";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("errorMessage", result);
        }

        [Test]
        public void Extract_Varable_Name_6()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Html.Raw(ViewBag.factPageDict)";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("HtmlRawViewBagfactPageDict", result);
        }

        [Test]
        public void Extract_Varable_Name_7()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Model.MessageList[\"MSG1005884\"]";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ModelMessageListMSG1005884", result);
        }

        [Test]
        public void Extract_Varable_Name_8()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Url.Action(\"MenuAssociation\", \"ConfigureMenu\")";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("UrlActionMenuAssociationConfigureMenu", result);
        }

        [Test]
        public void Extract_Varable_Name_9()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@Convert.ToInt32(ViewBag.PageNo);";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ConvertToInt32ViewBagPageNo", result);
        }

        [Test]
        public void Extract_Varable_Name_10()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@(new HtmlString(Json.Encode(Model.Frequency)));";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("newHtmlStringJsonEncodeModelFrequency", result);
        }

        [Test]
        public void Extract_Varable_Ignore_Plus_Sign()
        {
            // Arrange
            var evaluator = new JsVariableNameEvaluator();
            string input = "@ViewBag.PageNumber + 1;";

            // Act
            string result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual("ViewBagPageNumber1", result);
        }
    }
}
