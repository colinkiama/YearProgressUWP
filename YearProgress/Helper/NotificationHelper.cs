using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace YearProgress.Helper
{
    public class NotificationHelper
    {
        private const string _milestoneTaskEntryPoint = "Notifications.Notifications";
        private const string _timeZoneTaskEntryPoint = "Notifications.TimeZoneChange";
        private const int _minutesInADay = 1440;


        public void SendTutorialNotifcation()
        {

        }


        public void RegisterBackgroundTasks()
        {

            RegisterMilestoneTask();
            RegisterTimeZoneChangeTask();

           


        }

        private void RegisterTimeZoneChangeTask()
        {
            bool taskRegistered = false;
            string taskName = "timeZoneTask";

            foreach (var registeredTask in BackgroundTaskRegistration.AllTasks)
            {
                if (registeredTask.Value.Name == taskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            var builder = new BackgroundTaskBuilder();

            builder.Name = taskName;
            builder.TaskEntryPoint = _timeZoneTaskEntryPoint;
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
            BackgroundTaskRegistration task = builder.Register();
        }

        private void RegisterMilestoneTask()
        {
            bool taskRegistered = false;
            string taskName = "milestoneTask";

            foreach (var registeredTask in BackgroundTaskRegistration.AllTasks)
            {
                if (registeredTask.Value.Name == taskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            var builder = new BackgroundTaskBuilder();

            builder.Name = taskName;
            builder.TaskEntryPoint = _milestoneTaskEntryPoint;
            builder.SetTrigger(new TimeTrigger(_minutesInADay, false));

            builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

            BackgroundTaskRegistration task = builder.Register();
        }
    }

}
