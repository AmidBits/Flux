namespace Flux.Wpf.IValueConverter
{
  public class StringFormat : ValueConverter
  {
    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      string format = parameter as string ?? string.Empty;
      if (!string.IsNullOrEmpty(format))
        return string.Format(culture, format, value);
      return value.ToString()!;
    }
  }
}
