using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace calcUI
{
    [TestClass]
    public class CalculatorTests
    {
        private CalculatorService calculatorService;

        [TestInitialize]
        public void Initialize()
        {
            calculatorService = new CalculatorService();
            calculatorService.StartCalculator();
        }

        

        [TestMethod]
        public void TestAddition()
        {

            // Perform the addition operation
            int result = calculatorService.Add(2, 2);

            // Assert the result
            Assert.AreEqual("4", result);

        }


        [TestCleanup]
        public void Cleanup()
        {
            calculatorService.CloseCalculator();
        }
    }
}
