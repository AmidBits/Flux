namespace Flux.Wpf.IValueConverter
{
  public class Polynomial : ValueConverter
  {
    /// <summary>Gets or sets the coefficients of the polynomial.</summary>
    public System.Windows.Media.DoubleCollection Coefficients { get; set; } = default!;

    public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      double output = 0;

      for (int i = Coefficients.Count - 1; i >= 0; i--)
        output += Coefficients[i] * System.Math.Pow((double)value, (Coefficients.Count - 1) - i);

      return output;
    }
  }
}
