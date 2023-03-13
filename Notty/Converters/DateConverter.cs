using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Notty.Converters
{
    internal class DateConverter:MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime test = (DateTime)value;
                if (test.Date == DateTime.Now.Date) return test.ToString("HH:mm", culture);
                else return test.ToString("dd MMMM", culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) => Converter;

        private static readonly DateConverter Converter = new();
    }
}
