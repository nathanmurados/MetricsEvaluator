using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class LinqTests
    {
        [Test]
        public void AssertThatDistictWorks()
        {
            // Just a quick test of the de-duplication of a list of objects based on an object property value.
            // See JsModuleViewModel.Equals() override.
            // Without the override the items below wouldn't be considered duplicates because by default the equality is based on reference to objects.

            // Arrange
            List<JsModuleViewModel> totalRazorLines = new List<JsModuleViewModel>
            {
                new JsModuleViewModel {OriginalRazorText = "'@serverVariable1'", JavaScriptName = "serverVariable1"},
                new JsModuleViewModel {OriginalRazorText = "'@serverVariable1'", JavaScriptName = "serverVariable1"},
                new JsModuleViewModel {OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2"},
                new JsModuleViewModel {OriginalRazorText = "'@serverVariable3'", JavaScriptName = "serverVariable3"},
                new JsModuleViewModel {OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2"}
            };

            // Act
            totalRazorLines = totalRazorLines.Distinct().ToList();

            // Assert
            // To illustrate, without the equals override, this wouldn't work because although the values match they are different objects.
            Assert.IsTrue(totalRazorLines.Contains(new JsModuleViewModel { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" }));

            Assert.AreEqual(3, totalRazorLines.Count);
            Assert.AreEqual(totalRazorLines[0].JavaScriptName, "serverVariable1");
            Assert.AreEqual(totalRazorLines[1].JavaScriptName, "serverVariable2");
            Assert.AreEqual(totalRazorLines[2].JavaScriptName, "serverVariable3");
        }
    }
}