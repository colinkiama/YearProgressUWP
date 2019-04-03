using Microsoft.Services.Store.Engagement;
using Notifications.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YearProgress.Core;
using YearProgress.Helpers;
using YearProgress.View;

namespace YearProgress
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        public static FirstStartUpHelper startupHelper = new FirstStartUpHelper();
        public App()
        {
            this.InitializeComponent();
            this.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;
            this.Suspending += OnSuspending;
            Window.Current.Activated += Current_Activated;
        }

       

        private void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                rootFrame.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            await appStartup();
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }


            rootFrame.Navigate(typeof(MainPage), e.Arguments);

            // Ensure the current window is active
            Window.Current.Activate();

        }



        private void UpdateTiles()
        {
            new Notifications.Tiles().SendTileNotification();
        }

        private async Task appStartup()
        {
            ReviewHelper.TrackAppLaunch();
            AdjustWindowSettings();
            AdjustSettingsForAppVersion();
            await RegisterForDevCenterNotifcationsAsync();
            RegisterBackgroundTask();
            UpdateTiles();
        }

        private void AdjustSettingsForAppVersion()
        {
            var appVersionStatus = Mango.App.appVersionChecker.getAppVersionStatus();
            switch (appVersionStatus)
            {
                case Mango.Enums.appVersionStatus.FirstTime:
                    NotificationHelper.SendTutorialNotifcation();
                    InitalizeYearProgress();
                    startupHelper.shouldAskForTilePinning = true;
                    break;
                case Mango.Enums.appVersionStatus.Old:   
                case Mango.Enums.appVersionStatus.Current:
                    break;

            }
        }

        private void InitalizeYearProgress()
        {
            SettingsHelper settingsHelper = new SettingsHelper();
            settingsHelper.ResetProgressValueSettings();
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            await appStartup();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;



                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }



            rootFrame.Navigate(typeof(MainPage), args);

            // Ensure the current window is active
            Window.Current.Activate();
        }



        private void RegisterBackgroundTask()
        {
            new NotificationHelper().RegisterBackgroundTasks();
        }

        private async Task RegisterForDevCenterNotifcationsAsync()
        {
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.RegisterNotificationChannelAsync();
        }

        private void AdjustWindowSettings()
        {
            var appView = ApplicationView.GetForCurrentView();
            switch (DeviceDetection.DetectDeviceType())
            {
                case Enums.DeviceType.Phone:
                    appView.SuppressSystemOverlays = true;
                    appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
                    break;
                case Enums.DeviceType.Desktop:
                    appView.TitleBar.BackgroundColor = Colors.Transparent;
                    appView.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                    break;
                default:
                    break;
            }



        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
