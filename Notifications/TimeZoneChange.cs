using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Notifications
{
    
    public sealed class TimeZoneChange : IBackgroundTask 
    {
        BackgroundTaskDeferral _deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {


            _deferral.Complete();
        }
    }
}
