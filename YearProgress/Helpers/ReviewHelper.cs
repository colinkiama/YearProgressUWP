using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace YearProgress.Helpers
{
    public static class ReviewHelper
    {
        const string launchCountSettingsValue = "launchCount";
        const string noMorePromptsSettingsValue = "noMorePrompts";
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        // Recommended: Run this at app launch/activation
        public static void TrackAppLaunch()
        {
            TryUpdateLaunchCount();
        }

        private static void TryUpdateLaunchCount()
        {
            if (localSettings.Values[noMorePromptsSettingsValue] == null)
            {
                localSettings.Values[noMorePromptsSettingsValue] = false;
                localSettings.Values[launchCountSettingsValue] = (byte)1;
            }

            else if ((bool)localSettings.Values[noMorePromptsSettingsValue] == false)
            {
                {
                    byte oldValue = (byte)localSettings.Values[launchCountSettingsValue];
                    localSettings.Values[launchCountSettingsValue] = (byte)(oldValue + 1);
                }
            }
        }

        // Recommended: Run this when you navigate app's "Home Page" or "Shell".
        public static async Task TryRequestReview()
        {
            if (CheckIfTimeForReview())
            {
                var reviewDialog = new ContentDialog()
                {
                    Title = "Enjoying the app?",
                    Content = "It would be lovely if you left a review! 😊",
                    PrimaryButtonText = "Review",
                    SecondaryButtonText = "Never",
                    CloseButtonText = "Later",

                };

                reviewDialog.Closed += ReviewDialog_Closed;
                await reviewDialog.ShowAsync();
            }
        }

        private static async void ReviewDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                RemovePromptsForever();
                await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9PDQ5MLJFVSX"));
            }
            else
            {
                if (args.Result == ContentDialogResult.Secondary)
                {
                    RemovePromptsForever();
                }
                await ShowFeedbackDialog();
            }
        }

        private static void RemovePromptsForever()
        {
            localSettings.Values[noMorePromptsSettingsValue] = true;
        }

        private async static Task ShowFeedbackDialog()
        {
            var feedbackDialog = new ContentDialog()
            {
                Title = "Feedback",
                Content = "Your feedback is valuable. It improves your experience!",
                PrimaryButtonText = "Give Feedback",
                CloseButtonText = "Later",
            };

            feedbackDialog.Closed += FeedbackDialog_Closed;
            await feedbackDialog.ShowAsync();
        }

        private async static void FeedbackDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                await Launcher.LaunchUriAsync(new Uri("mailto:colinkiama@gmail.com?subject=Year%20Progress%20Feedback&body=<Write%20your%20feedback%20here>"));
            }
        }

        private static bool CheckIfTimeForReview()
        {
            bool isTimeToReview = false;
            if ((bool)localSettings.Values[noMorePromptsSettingsValue] == false)
            {
                if ((byte)localSettings.Values[launchCountSettingsValue] == 3)
                {
                    isTimeToReview = true;
                    localSettings.Values[launchCountSettingsValue] = (byte)0;
                }
            }

            return isTimeToReview;
        }
    }
}
