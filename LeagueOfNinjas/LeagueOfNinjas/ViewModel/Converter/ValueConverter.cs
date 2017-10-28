using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LeagueOfNinjas.ViewModel;

namespace LeagueOfNinjas
{
    public class ValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = 0;
            for (int i = 0; i < 3; i++)
            {
                value += (int)values[i];
            }
            value = (value / (int) values[3]);
            return value.ToString("F2");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
