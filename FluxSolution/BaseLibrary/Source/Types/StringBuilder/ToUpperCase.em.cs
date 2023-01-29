namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpperCase(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToUpper(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }

    /// <summary>Convert all characters to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpperCase(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
      => ToUpperCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), culture);
  }
}
