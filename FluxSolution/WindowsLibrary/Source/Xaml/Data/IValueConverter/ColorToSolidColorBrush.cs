namespace Flux.Wpf.IValueConverter
{
  /// <summary>Conversion between 'Windows.UI.Color' and 'Windows.UI.Xaml.Media.SolidColorBrush'.</summary>
  public class ColorToSolidColorBrush : ValueConverter
  {
    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return UniversalConversion(value, targetType, parameter, culture, nameof(Convert));
    }
    public override object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return UniversalConversion(value, targetType, parameter, culture, nameof(ConvertBack));
    }

    private static object UniversalConversion(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture, string caller)
    {
      if (value is System.Windows.Media.Color color && targetType == typeof(System.Windows.Media.SolidColorBrush))
        return new System.Windows.Media.SolidColorBrush(color);

      if (value is System.Windows.Media.SolidColorBrush && targetType == typeof(System.Windows.Media.Color))
        return ((System.Windows.Media.SolidColorBrush)value).Color;

      throw new System.NotSupportedException(string.Format("{0} from type '{1}' to type '{2}'.'.", caller, value.GetType().FullName, targetType.FullName));
    }
  }

  /*
	public sealed class MathParser
		: ValueConverter
	{
		public string ConvertBackDelimiter { get; set; }
		public string ValuePlaceholder { get; set; }

		public MathParser()
			: base()
		{
			ConvertBackDelimiter = "|";
			ValuePlaceholder = "#";
		}

		public override object Convert(object value, System.Type targetType, object parameter, string language)
		{
			string p = parameter.ToString();
			if (p.Contains(ConvertBackDelimiter))
				p = p.Substring(0, p.IndexOf(ConvertBackDelimiter));
			if (p.Contains(ValuePlaceholder))
				p = p.Replace(ValuePlaceholder, value.ToString());
			return new Flux.Modeling.MathParser.LambdaExpressions().Solve(p);
		}
		public override object ConvertBack(object value, System.Type targetType, object parameter, string language)
		{
			string p = parameter.ToString();
			if (p.Contains(ConvertBackDelimiter))
				p = p.Substring(p.IndexOf(ConvertBackDelimiter) + 1);
			if (p.Contains(ValuePlaceholder))
				p = p.Replace(ValuePlaceholder, value.ToString());
			return new Flux.Modeling.MathParser.LambdaExpressions().Solve(p);
		}
	}
	*/
}
