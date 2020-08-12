namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Convert all characters to lower case using the specified culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        var sourceChar = source[index];
        var targetChar = char.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
    /// <summary>Convert all characters to lower case using the current culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source)
      => ToLower(source, System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Convert all characters to upper case using the specified culture.</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        var sourceChar = source[index];
        var targetChar = char.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
    /// <summary>Convert all characters to upper case using the current culture.</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source)
      => ToLower(source, System.Globalization.CultureInfo.CurrentCulture);
  }
}
