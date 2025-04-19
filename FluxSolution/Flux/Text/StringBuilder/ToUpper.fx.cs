namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Converts characters to upper-case in <paramref name="source"/>, starting at <paramref name="index"/> for <paramref name="length"/> characters, to upper-case. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);
      System.ArgumentOutOfRangeException.ThrowIfNegative(length);

      cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var i = index + length - 1; i >= index; i--)
        if (source[i] is var sourceChar && char.ToUpper(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
          source[i] = targetChar;

      return source;
    }

    /// <summary>
    /// <para>Converts characters to upper-case in <paramref name="source"/>, starting at <paramref name="index"/> to the end. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
      => ToUpper(source, index, (source?.Length ?? 0) - index, cultureInfo);

    /// <summary>
    /// <para>Converts characters to upper-case in <paramref name="source"/>. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Text.StringBuilder ToUpper(this System.Text.StringBuilder source, System.Globalization.CultureInfo? cultureInfo = null)
      => ToUpper(source, 0, cultureInfo);
  }
}
