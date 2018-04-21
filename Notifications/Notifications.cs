using Microsoft.Toolkit.Uwp.Notifications;
using Notifications.Helpers;
using Notifications.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace Notifications
{
    public sealed class Notifications : IBackgroundTask
    {
        int savedProgress;
        bool isGreaterThanSavedProgress = false;
        DateCalc dateCalculation = new DateCalc();
        SettingsHelper settingsHelper = new SettingsHelper();
        public void Run(IBackgroundTaskInstance taskInstance)
        {

            int yearProgress = dateCalculation.yearProgressPercentage;

            isGreaterThanSavedProgress = IsHigherThanSavedProgress(yearProgress);

            if (isGreaterThanSavedProgress)
            {
                if (IsRegularInterval(yearProgress))
                {
                    SendAMilestoneNotification(yearProgress);

                }
            }

            StoreYearProgressIfNew(yearProgress);

        }

        private void StoreYearProgressIfNew(int yearProgress)
        {
            if (isGreaterThanSavedProgress)
            {
                settingsHelper.SetYearProgress(yearProgress);
            }
        }

        private bool IsHigherThanSavedProgress(int yearProgress)
        {
            savedProgress = settingsHelper.GetStoredYearProgress();
            isGreaterThanSavedProgress = yearProgress > savedProgress;
            return isGreaterThanSavedProgress;
        }

        private void SendAMilestoneNotification(int yearProgress)
        {

            SendRegularMilestoneNotification(yearProgress);

        }

        public bool IsRegularInterval(int yearProgress)
        {
            const int regularInterval = 5;

            bool progressIsRegularInterval = false;


            if (yearProgress > 0)
            {
                if (yearProgress % regularInterval == 0)
                {
                    progressIsRegularInterval = true;
                }

            }

            return progressIsRegularInterval;
        }





        public void SendRegularMilestoneNotification(int yearProgress)
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
                    Text = $"{dateCalculation.currentDate.Year} Is {yearProgress}% Complete!"
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


    }
}
