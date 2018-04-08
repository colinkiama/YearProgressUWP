using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YearProgress.Model;

namespace YearProgress.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ViewModelLoaded;
        public DateCalc DateCalcObject { get; set; }
        private int _yearProgress;

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
            DateCalcObject = new DateCalc();
            ViewModelLoaded?.Invoke(this, EventArgs.Empty);
        }

    }
}
