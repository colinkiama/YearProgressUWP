﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace YearProgress.Converters
{
    public class PercentageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
                return value + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString().Last() != '%')
            {
                return value + "%";
            }

            else
            {
                return value;
            }

        }
    }
}
