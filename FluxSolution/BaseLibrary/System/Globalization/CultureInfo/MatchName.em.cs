namespace Flux
{
  public static partial class Fx
  {
    public static bool TryMatchCulture(this System.Globalization.CultureInfo source, string cultureName, out int hierarchy)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (hierarchy = 0; source != System.Globalization.CultureInfo.InvariantCulture; hierarchy++)
      {
        if (System.Text.RegularExpressions.Regex.IsMatch(cultureName, @"(?<=(^|[^\p{L}]))" + source.Name.Replace('-', '.') + @"(?=([^\p{L}]|$))", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
          return true;

        source = source.Parent;
      }

      hierarchy = -1;
      return false;
    }
  }
}
