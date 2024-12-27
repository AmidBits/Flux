namespace Flux.Wpf.IValueConverter
{
  public class AnyConverter : ValueConverter
  {
    public string FormatStringForToString { get; set; } = default!;
    public string MatchPatternForParse { get; set; } = default!;

    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value.TryTypeConverter(out var result, null, targetType))
      {
        return result;
      }

      return base.Convert(value, targetType, parameter, culture);
    }
    public override object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value.TryTypeConverter(out var result, null, targetType))
      {
        return result;
      }

      return base.ConvertBack(value, targetType, parameter, culture);
    }
  }
}
