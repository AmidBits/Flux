namespace Flux
{
  public static partial class Fx
  {
    public static bool TryMatchName(this System.Globalization.CultureInfo source, string name)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return System.Text.RegularExpressions.Regex.IsMatch(name, $"^{source.Name.Replace('-', '.')}$", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        || (name.Length == 2 && string.Equals(name, source.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase))
        || (name.Length == 3 && (string.Equals(name, source.ThreeLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase) || string.Equals(name, source.ThreeLetterWindowsLanguageName, StringComparison.InvariantCultureIgnoreCase)))
      ;
    }
  }
}
