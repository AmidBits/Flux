namespace Flux.Wpf.IValueConverter
{
  public class Scale : ValueConverter
  {
    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return System.Convert.ToDouble(parameter) * System.Convert.ToDouble(value);
    }
    public override object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return System.Convert.ToDouble(parameter) / System.Convert.ToDouble(value);
    }
  }
}
