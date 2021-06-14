namespace Flux
{
  public static partial class SystemTextStringBuilderEm
  {
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLowerCase(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo culture)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (culture is null) throw new System.ArgumentNullException(nameof(culture));

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the current culture.</summary>
    public static System.Text.StringBuilder ToLowerCase(this System.Text.StringBuilder source, int startIndex, int length)
      => ToLowerCase(source, startIndex, length, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the invariant culture.</summary>
    public static System.Text.StringBuilder ToLowerCaseInvariant(this System.Text.StringBuilder source, int startIndex, int length)
      => ToLowerCase(source, startIndex, length, System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Convert all characters to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLowerCase(this System.Text.StringBuilder source, System.Globalization.CultureInfo culture)
      => ToLowerCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), culture);
    /// <summary>Convert all characters to lower case. Uses the current culture.</summary>
    public static System.Text.StringBuilder ToLowerCaseCurrent(this System.Text.StringBuilder source)
      => ToLowerCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Convert all characters to lower case. Uses the invariant culture.</summary>
    public static System.Text.StringBuilder ToLowerCaseInvariant(this System.Text.StringBuilder source)
      => ToLowerCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), System.Globalization.CultureInfo.InvariantCulture);
  }
}
