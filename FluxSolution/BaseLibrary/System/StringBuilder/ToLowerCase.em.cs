namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLowerCase(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }

    /// <summary>Convert all characters to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLowerCase(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
      => ToLowerCase(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), culture);
  }
}
