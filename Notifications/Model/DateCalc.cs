using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Model
{
    public sealed class DateCalc
    {

        private DateTime _currentDate;
        private DateTime _newYearDate;

        // Now uses DateTimeOffset instead of DateTime for public properties because DateTime isn't supported by the Windows Runtime
        public DateTimeOffset currentDate
        {
            get { return new DateTimeOffset(_currentDate); }
        }

        public DateTimeOffset newYearDate
        {
            get { return new DateTimeOffset(_newYearDate); }
        }

        public int yearProgressPercentage { get; private set; }

        public DateCalc()
        {
            _currentDate = DateTime.Now;
            _newYearDate = calculateNewYearDate();
            yearProgressPercentage = calculateYearProgress();
        }

        private int calculateYearProgress()
        {
            DateTime beginningOfYearDate = new DateTime(_currentDate.Year, 1, 1);


            var currentDateSpan = TimeSpan.FromTicks(_currentDate.Ticks);
            var newYearDateSpan = TimeSpan.FromTicks(_newYearDate.Ticks);
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
            TimeSpan dateDifference = _newYearDate - _currentDate;
            return dateDifference;
        }

        private DateTime calculateNewYearDate()
        {
            return new DateTime(_currentDate.Year + 1, 1, 1);
        }



    }
}
