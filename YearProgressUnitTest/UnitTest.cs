
using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YearProgressUnitTest
{
    [TestClass]
    public class DateCalcTests
    {
        DateTime currentDate = new DateTime(2018,4,8);
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
            DateTime beginningOfYearDate = new DateTime(currentDate.Year, 1, 1);


            var currentDateSpan = TimeSpan.FromTicks(currentDate.Ticks);
            var newYearDateSpan = TimeSpan.FromTicks(newYearDate.Ticks);
            var currentYearDateSpan = TimeSpan.FromTicks(beginningOfYearDate.Ticks);

            double currentDateDays = currentDateSpan.TotalDays;
            double newYearDays = newYearDateSpan.TotalDays;
            double currentYearDays = currentYearDateSpan.TotalDays;

            double totalDaysDifference = newYearDays - currentDateDays;

            double yearLengthInDays = newYearDays - currentYearDays;

            double daysThatHaveWentBySinceYearStart = yearLengthInDays - totalDaysDifference;

            double percentageAsDouble = daysThatHaveWentBySinceYearStart / yearLengthInDays * 100;
            

            Assert.IsTrue(percentageAsDouble < 100 && percentageAsDouble > 0, $"Percentage is greater than 100% ({percentageAsDouble}%)");

            int percentageAsInteger = (int)Math.Floor(percentageAsDouble);
            Assert.IsTrue(percentageAsInteger < 100 && percentageAsInteger > 0, "Potential integer problems. Please debug.");

        }

        
    }
}
