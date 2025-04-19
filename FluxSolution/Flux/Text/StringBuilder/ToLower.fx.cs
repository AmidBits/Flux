namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);
      System.ArgumentOutOfRangeException.ThrowIfNegative(length);

      cultureInfo ??= System.Globalization.CultureInfo.InvariantCulture;

      for (var i = index + length - 1; i >= index; i--)
        if (source[i] is var sourceChar && char.ToLower(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
          source[i] = targetChar;

      return source;
    }

    /// <summary>Converts the letter at <paramref name="index"/> to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
      => ToLower(source, index, (source?.Length ?? 0) - index, cultureInfo);

    /// <summary>Convert all characters to lower case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToLower(this System.Text.StringBuilder source, System.Globalization.CultureInfo? cultureInfo = null)
      => ToLower(source, 0, cultureInfo);
  }
}
