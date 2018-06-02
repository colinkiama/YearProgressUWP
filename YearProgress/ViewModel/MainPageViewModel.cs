using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml;
using YearProgress.Model;

namespace YearProgress.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ViewModelLoaded;
        public DateCalc DateCalcObject { get; set; }
        private int _yearProgress;

        public delegate void ClickHandler(object sender, RoutedEventArgs e);
        public ClickHandler myClickHandler;
        public ClickHandler shareButtonHandler;

       public Uri twitterUri = new Uri("https://www.twitter.com/colinkiama");
        public Uri gitHubUri = new Uri("https://www.github.com/colinkiama");
        public Uri emailUri = new Uri("mailto:colinkiama@hotmail.co.uk");

        public int YearProgress
        {
            get { return DateCalcObject.yearProgressPercentage; }
            set
            {
                _yearProgress = value;
                RaisePropertyChanged(nameof(YearProgress));
            }
        }

        private void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainPageViewModel()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            DateCalcObject = new DateCalc();
            ViewModelLoaded?.Invoke(this, EventArgs.Empty);
            myClickHandler = new ClickHandler(FeedbackButton_Click);
            shareButtonHandler = new ClickHandler(ShareButton_Click);
        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            
            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetText($"{DateCalcObject.currentDate.Year} is {YearProgress}% complete! - Shared Via Year Progress: https://bit.ly/2JcQEfE");
            request.Data.Properties.Title = "Year Progress";
            request.Data.Properties.Description = "Effortlessly share the progress of the year to other apps";
            
        }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(emailUri);
        }


    }
}
