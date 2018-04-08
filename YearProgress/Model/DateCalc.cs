using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearProgress.Model
{
    public class DateCalc
    {
        public DateTime currentDate { get; set; }
        public DateTime newYearDate { get;  set; }
        public int yearProgressPercentage { get; set; }

        public DateCalc()
        {
            currentDate = DateTime.Now;
            newYearDate = calculateNewYearDate();
            yearProgressPercentage = calculateYearProgress();
        }

        private int calculateYearProgress()
        {
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

            return (int)Math.Floor(percentageAsDouble);
        }

        private TimeSpan GetDifferenceFromNewYear()
        {
            TimeSpan dateDifference = newYearDate - currentDate;
            return dateDifference;
        }

        private DateTime calculateNewYearDate()
        {
             return new DateTime(currentDate.Year + 1, 1, 1);
        }

        public int getNewYear()
        {
            return newYearDate.Year;
        }

    }
}
