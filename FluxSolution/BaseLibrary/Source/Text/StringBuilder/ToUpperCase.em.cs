namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpperCase(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (culture is null) throw new System.ArgumentNullException(nameof(culture));

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToUpper(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the current culture.</summary>
    public static System.Text.StringBuilder ToUpperCase(this System.Text.StringBuilder source, int startIndex, int length)
      => ToUpperCase(source, startIndex, length, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the invariant culture.</summary>
    public static System.Text.StringBuilder ToUpperCaseInvariant(this System.Text.StringBuilder source, int startIndex, int length)
      => ToUpperCase(source, startIndex, length, System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Convert all characters to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpperCase(this System.Text.StringBuilder source, System.Globalization.CultureInfo culture)
      => ToUpperCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), culture);
    /// <summary>Convert all characters to upper case. Uses the current culture.</summary>
    public static System.Text.StringBuilder ToUpperCaseCurrent(this System.Text.StringBuilder source)
      => ToUpperCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Convert all characters to upper case. Uses the invariant culture.</summary>
    public static System.Text.StringBuilder ToUpperCaseInvariant(this System.Text.StringBuilder source)
      => ToUpperCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), System.Globalization.CultureInfo.InvariantCulture);
  }
}
