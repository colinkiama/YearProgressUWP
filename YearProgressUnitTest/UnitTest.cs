
using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YearProgressUnitTest
{
    [TestClass]
    public class DateCalcTests
    {
        DateTime currentDate = DateTime.Now;
        DateTime newYearDate;
        public void Do()
        {
            DifferenceBetweenDatesTest();
            PercentageDateTest();
        }

        [TestMethod]
        public void DifferenceBetweenDatesTest()
        {
            setNewYearDate();
           
            TimeSpan dateDifference = newYearDate - currentDate;
            Assert.IsTrue(dateDifference.Ticks > 0, $"Error: Date difference is {dateDifference.Ticks}");
            
        }

        private void setNewYearDate()
        {
            newYearDate = new DateTime(currentDate.Year + 1, 1, 1);
        }

        [TestMethod]
        public void PercentageDateTest()
        {
            setNewYearDate();
            long percentageAsDouble = (currentDate.Ticks / newYearDate.Ticks);
            Assert.IsTrue(percentageAsDouble < 100, $"Percentage is greater than 100% ({percentageAsDouble}%)");

        }

        
    }
}
