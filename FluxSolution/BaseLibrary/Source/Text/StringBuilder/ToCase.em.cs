namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Convert all characters to lower case using the specified culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }

    /// <summary>Convert all characters to upper case using the specified culture.</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToUpper(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
  }
}
