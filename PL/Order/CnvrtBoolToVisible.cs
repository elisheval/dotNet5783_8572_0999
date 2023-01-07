using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Order;

//[ValueConversion(typeof(bool), typeof(Visibility))]
public class CnvrtBoolToVisible : IValueConverter
{
    //המרה ממקור ליעד
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //value: הערך שאיתו הגענו לפונקציה, כאן בוליאני
        //יחזור הערך לאחר המרה
        if ((bool)value == true)
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    //המרה מיעד למקור
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
