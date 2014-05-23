using System.Collections.Generic;
using NUnit.Framework;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsModuleLineEvaluatorTests
    {
        
        [Test]
        public void Extract_Razor_Quoted_ViewBag()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "var selectedMenu = '@ViewBag.MenuInstanceName';";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("'@ViewBag.MenuInstanceName'", result[0].Text);
        }

        [Test]
        public void Extract_Razor_Double_Quoted_ViewBag()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "$(completionStatus).val(\"@Model.OtherDetails.PageCompletionStatus\");";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("\"@Model.OtherDetails.PageCompletionStatus\"", result[0].Text);
        }


        [Test]
        public void Extract_Razor_Single_Quoted_UrlAction()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("'@Url.Action(\"Configure\", \"ConfigureMenu\")'", result[0].Text);
        }

        [Test]
        public void Extract_Razor_Double_Quoted_UrlAction()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "var url = \"@Url.Action(\"LoadTrendAnalysisChart\", \"WidgetGallery\", new { communityId = \"_Id\", startYear = \"_Sdate\", endYear = \"_eDate\", trendParametr = \"_trend\" })\";";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("\"@Url.Action(\"LoadTrendAnalysisChart\", \"WidgetGallery\", new { communityId = \"_Id\", startYear = \"_Sdate\", endYear = \"_eDate\", trendParametr = \"_trend\" })\";", result[0].Text);
        }


        [Test]
        public void Extract_Razor_Quoted_JQuery_val()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "$('#DecommisionReason').val('@decommisionReason');";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("'@decommisionReason'", result[0].Text);
        }
        [Test]
        public void Extract_Razor_Not_Quoted()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Unquoted);
            Assert.AreEqual("@Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList))", result[0].Text);
        }

        // Can't cope with
        //[Test]
        public void Extract_Razor_Quoted_But_Contains_Spaces_1()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "if (\"@(Model.OtherDetails.ApplicantContactDetails == null)\" != \"true\") {";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@(Model.OtherDetails.ApplicantContactDetails", result[0].Text);
        }

        [Test]
        public void Extract_Razor_2_Quoted_Fragments()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();

            // NB contains 2 fragments of razor
            // data: {'docId1':'" + '@ViewBag.docid' + "','conditionType1':'" + '@ViewBag.doctype' + "'}

            string input = " data: \"{'docId1':'\" + '@ViewBag.docid' + \"','conditionType1':'\" + '@ViewBag.doctype' + \"'}\",";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.IsTrue(result[1].FragType == FragType.Quoted);
            Assert.AreEqual("'@ViewBag.docid'", result[0].Text);
            Assert.AreEqual("'@ViewBag.doctype'", result[1].Text);
        }

        [Test]
        public void Extract_Razor_Quoted_ConvertToString()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "$('#HiddenName').val('@Convert.ToString(stateWatcherVM.LName)');";

            // Act
            List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("'@Convert.ToString(stateWatcherVM.LName)'", result[0].Text);
        }


        /// <summary>
        /// Extract a razor fragment that is not immediately bounded by quotes.
        /// It is still within quotes, but there's some text between the quote and the razor fragment.
        /// This is not just an issue for extracting the fragment, but for how we modify the line.
        /// 
        /// For example: alert('text left @Viewbag.Variable');
        /// Will become: alert('text left' + ap2.ViewbagVariable);
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_to_left_Exception()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable');";

            // Act
            //List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            //Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            //Assert.AreEqual("REQUIRES MANUAL CHECK", result[0].Text);
        }
        
        /// <summary>
        /// Text to right. 
        /// For example: alert('@Viewbag.Variable right text');
        /// Will become: alert(ap2.ViewbagVariable + 'right text');/// 
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_to_right_Exception()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "alert('@Viewbag.Variable text right');";

            // Act
            //List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            //Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            //Assert.AreEqual("REQUIRES MANUAL CHECK", result[0].Text);
        }

        /// <summary>
        /// Text either side.
        // For example: alert('text left @Viewbag.Variable right text');
        /// Will become: alert('text left' + ap2.ViewbagVariable + 'right text');
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_eitherside_Exception()
        {
            // Arrange
            var evaluator = ProcessorsToTest.GetJsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable text right');";

            // Act
            //List<Fragment> result = evaluator.Evaluate(input);

            // Assert
            Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            //Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            //Assert.AreEqual("REQUIRES MANUAL CHECK", result[0].Text);
        }
    }
}
