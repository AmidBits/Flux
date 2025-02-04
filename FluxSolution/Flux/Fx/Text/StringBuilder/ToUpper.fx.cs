namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts the letter at <paramref name="index"/> to upper-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[index] = char.ToUpper(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
        if (source[index] is var sourceChar && char.ToUpper(sourceChar, culture) is var targetChar && sourceChar != targetChar)
          source[index] = targetChar;

      return source;
    }

    /// <summary>Convert all characters to upper case. Uses the specified culture.</summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return ToUpper(source, 0, source.Length, culture);
    }
  }
}
