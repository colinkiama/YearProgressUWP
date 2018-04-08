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
            var currentDateTimeSpan = TimeSpan.FromTicks(currentDate.Ticks);
            var newYearDateTimeSpan = TimeSpan.FromTicks(newYearDate.Ticks);

            

            double currentDateDays = currentDateTimeSpan.TotalMilliseconds;
            double newYearDateDays = newYearDateTimeSpan.TotalMilliseconds;

            double progressAsDouble = (currentDateDays / newYearDateDays) * (double)100;

            
       
            return (int)Math.Round(progressAsDouble);
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
