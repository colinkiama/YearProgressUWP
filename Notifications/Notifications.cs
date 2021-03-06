﻿using Microsoft.Toolkit.Uwp.Notifications;
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
        bool hasSavedProgressChanged = false;
        DateCalc dateCalculation = new DateCalc();
        SettingsHelper settingsHelper = new SettingsHelper();
        public void Run(IBackgroundTaskInstance taskInstance)
        {

            int yearProgress = dateCalculation.yearProgressPercentage;

            hasSavedProgressChanged = isDifferentToSavedProgress(yearProgress);

            if (hasSavedProgressChanged)
            {
                SendAMilestoneNotification(yearProgress);
            }

            StoreYearProgressIfNew(yearProgress);

        }

        private void StoreYearProgressIfNew(int yearProgress)
        {
            if (hasSavedProgressChanged)
            {
                settingsHelper.SetYearProgress(yearProgress);
            }
        }

        private bool isDifferentToSavedProgress(int yearProgress)
        {
            savedProgress = settingsHelper.GetStoredYearProgress();
            hasSavedProgressChanged = yearProgress != savedProgress;
            return hasSavedProgressChanged;
        }

        private void SendAMilestoneNotification(int yearProgress)
        {

            SendRegularMilestoneNotification(yearProgress);

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
