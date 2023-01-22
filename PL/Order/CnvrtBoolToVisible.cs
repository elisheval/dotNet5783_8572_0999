using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Order;
public class CnvrtBoolToVisible : IValueConverter
{
    #region convert
    /// <summary>
    /// Implementation of conversion methods of the interface,
    /// Conversion from source to destination
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        if ((bool)value == true)
            return Visibility.Visible;
        return Visibility.Collapsed;
    }
    #endregion

    #region convert back
    /// <summary>
    /// Implementation of conversion methods of the interface,
    /// Conversion from destination to source   
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    #endregion
}
