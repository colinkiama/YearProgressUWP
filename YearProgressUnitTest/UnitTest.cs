
using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace YearProgressUnitTest
{
    [TestClass]
    public class DateCalcTests
    {
       public DateTime currentDate = new DateTime(2018,4,8);
       public DateTime newYearDate;
       public int percentage;
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

            percentage = percentageAsInteger;
        }

        
        
    }
    [TestClass]
    public class NotificationSettingsTests
    {
        public const int expepctedPercentage = 25;
        [TestMethod]
        public void FiveMinIntervalNotificationTest()
        {
            DateCalcTests dateCalcObject = new DateCalcTests();
            dateCalcObject.Do();
            int percentageDisplayed = 0;

            for (int i = 100; i > -1 ; i-=5)
            {
                
                if (dateCalcObject.percentage >= i)
                {
                    Logger.LogMessage($"{dateCalcObject.currentDate.Year} is {i}% complete");
                    percentageDisplayed = i;
                    break;
                }

                if (i == 100)
                {
                    if (dateCalcObject.percentage >= 99)
                    {
                       Logger.LogMessage($"{dateCalcObject.currentDate.Year} is 99% complete");
                       percentageDisplayed = i;
                    }
                }
            }
            
            Assert.IsTrue(percentageDisplayed == expepctedPercentage, $"Displyed percentage is {percentageDisplayed}");
        }
    }
}
