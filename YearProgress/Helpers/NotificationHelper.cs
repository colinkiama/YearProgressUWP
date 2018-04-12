using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace YearProgress.Helpers
{
    public class NotificationHelper
    {
        private const string _milestoneTaskEntryPoint = "Notifications.Notifications";
        private const string _timeZoneTaskEntryPoint = "Notifications.TimeZoneChange";
        private const string _tileTaskEntryPoint = "Notifications.Tiles";
        private const int _minutesInADay = 1440;


        internal static void SendTutorialNotifcation()
        {
            var toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
            {
                new AdaptiveText()
                {
                    Text = "Thanks for downloading this app!"
                },
                new AdaptiveText()
                {
                    Text = "We'll send progress milestone notifcations at 5% intervals and major milestone notifications at 25% intervals. "
                }
            }
                    }
                }
            };

            // Create the toast notification
            var toastNotif = new ToastNotification(toastContent.GetXml());

            // And send the notification
            ToastNotificationManager.CreateToastNotifier().Show(toastNotif);
        }


        public void RegisterBackgroundTasks()
        {

            RegisterMilestoneTask();
            RegisterTimeZoneChangeTask();
            RegisterTileNotification();



        }

        private void RegisterTileNotification()
        {
            bool taskRegistered = false;
            string taskName = "tileTask";

            foreach (var registeredTask in BackgroundTaskRegistration.AllTasks)
            {
                if (registeredTask.Value.Name == taskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();

                builder.Name = taskName;
                builder.TaskEntryPoint = _tileTaskEntryPoint;
                builder.SetTrigger(new TimeTrigger(60, false));
                BackgroundTaskRegistration task = builder.Register();
            }
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

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();

                builder.Name = taskName;
                builder.TaskEntryPoint = _timeZoneTaskEntryPoint;
                builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
                BackgroundTaskRegistration task = builder.Register();
            }
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

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();

                builder.Name = taskName;
                builder.TaskEntryPoint = _milestoneTaskEntryPoint;
                builder.SetTrigger(new TimeTrigger(_minutesInADay, false));

                builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

                BackgroundTaskRegistration task = builder.Register();

            }
        }
    }

}
