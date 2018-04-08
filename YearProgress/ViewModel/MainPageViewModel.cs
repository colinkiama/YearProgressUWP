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
        public DateCalc DateCalcObject { get; set; }

     
        public MainPageViewModel()
        {
            DateCalcObject = new DateCalc();
        }
       
    }
}
