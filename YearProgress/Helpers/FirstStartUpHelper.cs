using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearProgress.Helpers
{
    public class FirstStartUpHelper
    {
        public bool shouldAskForTilePinning { get; set; }

        public FirstStartUpHelper()
        {
            shouldAskForTilePinning = false;
        }
    }

}
