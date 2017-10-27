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
            int value = 0;
            for (int i = 0; i < values.Length; i++)
            {
                value += (int) ((double) values[i] / (double) parameter);
            }
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
