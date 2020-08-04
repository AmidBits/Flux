namespace Flux.Wpf.IValueConverter
{
  public class SecondsToTimeSpan : ValueConverter
  {
    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo language)
    {
      return System.TimeSpan.FromSeconds((double)value).ToString();
    }
    public override object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo language)
    {
      return System.TimeSpan.Parse((string)value);
    }
  }
}
