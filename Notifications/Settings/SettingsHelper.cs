using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Notifications.Helpers
{
    public sealed class SettingsHelper
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        const string progressKey = "currentProgress";

        public void ResetProgressValueSettings()
        {
            localSettings.Values[progressKey] = 0;
        }

        public void SetYearProgress(int progress)
        {
            localSettings.Values[progressKey] = progress;
        }

        public int GetStoredYearProgress()
        {
            return (int)localSettings.Values[progressKey];
        }
    }
}
