namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts the letter at <paramref name="index"/> to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[index] = char.ToLower(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
      => ToLower(source, 0, source?.Length ?? throw new System.ArgumentNullException(nameof(source)), culture);
  }
}
