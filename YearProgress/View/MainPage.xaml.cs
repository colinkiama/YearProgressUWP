using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UltraTimer;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YearProgress.Control;

// Remove this using statement or project will fail to build
using YearProgress.adKeys;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YearProgress.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool wasCopyButtonClicked = false;
        public MainPage()
        {
            this.InitializeComponent();


#if DEBUG
            //topAd.ApplicationId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
            //topAd.AdUnitId = "test";





#else
            // Remove or replace these lines or project will fail to run
            topAd.ApplicationId = AdKeys.appId;
            topAd.AdUnitId = AdKeys.adUnitId;

#endif

            ViewModel.CopyButtonClicked += ViewModel_CopyButtonClicked;
            Clipboard.ContentChanged += Clipboard_ContentChanged;
            
        }

        private void ViewModel_CopyButtonClicked(object sender, EventArgs e)
        {
            wasCopyButtonClicked = true;
        }

        private async void Clipboard_ContentChanged(object sender, object e)
        {
            if (wasCopyButtonClicked)
            {
                await ShowContentHasBeenCopied();
                wasCopyButtonClicked = false;
            }
        }

        private async Task ShowContentHasBeenCopied()
        {
            const string messageToDisplay = "Year Progress Has Been Copied!";
            TextBlock messageTextBlock = new TextBlock() { Text = messageToDisplay, HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.WrapWholeWords, VerticalAlignment = VerticalAlignment.Center};

            StackPanel messageStackPanel = new StackPanel() {CornerRadius = new CornerRadius(20),
                VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center,
            Background = new SolidColorBrush(Color.FromArgb(255, 14, 15, 14)),
            Padding = new Thickness(20,12,20,12)};


            messageStackPanel.Children.Add(messageTextBlock);

            rootGrid.Children.Add(messageStackPanel);

            await AnimateMessageStackPanel(messageStackPanel);

            rootGrid.Children.Remove(messageStackPanel);

        }

        private async Task AnimateMessageStackPanel(StackPanel messageStackPanel)
        {
            float stackPanelHeight = (float)messageStackPanel.ActualHeight;
            await messageStackPanel.Offset(0, -stackPanelHeight, 0).Fade(0,0).StartAsync();

            float animationYOffset = stackPanelHeight + 150;
            await messageStackPanel.Offset(0, animationYOffset).Fade(1).StartAsync();

            await messageStackPanel.Offset(0, -animationYOffset, delay: 500).Fade(0, delay: 500).StartAsync();
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
            double intervalAmount = (double)percentageToLoad/ numberOfIntervals;
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
