using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UltraTimer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YearProgress.Control;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YearProgress.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


#if DEBUG
            topAd.ApplicationId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
            topAd.AdUnitId = "test";
#else
            topAd.ApplicationId = "9pdq5mljfvsx";
            topAd.AdUnitId = "1100022477";

#endif
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AnimateProgressBar();
            if (App.startupHelper.shouldAskForTilePinning)
            {
                await new AskForLiveTilePinDialog().ShowAsync();
            }
           
        }

        

        private void AnimateProgressBar()
        {
            
            const int numberOfIntervals = 25;

            int percentageToLoad = ViewModel.YearProgress;
            int animationTimeInMiliseconds = 500;
            int intervalAmount = percentageToLoad/ numberOfIntervals;
            int intervalTime = animationTimeInMiliseconds / numberOfIntervals;

            Timer progressTimer = new Timer(new TimeSpan(0,0,0,0,animationTimeInMiliseconds), new TimeSpan(0,0,0,0,intervalTime));
            progressTimer.TimerTicked += (s, e) =>
            {
                PercentageProgressBar.Value += intervalAmount;
            };
            progressTimer.StartTimer();
            
        }

       
    }
}
