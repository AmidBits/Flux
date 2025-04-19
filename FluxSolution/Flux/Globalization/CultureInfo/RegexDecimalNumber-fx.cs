namespace Flux
{
  public static partial class CultureInfos
  {
    public static System.Text.RegularExpressions.Regex RegexDecimalNumber(this System.Globalization.CultureInfo source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return new($@"[-+]?[0-9]*\\{source.NumberFormat.NumberDecimalSeparator}?[0-9]+");
    }
  }
}
