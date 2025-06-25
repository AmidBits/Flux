namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Convert all chars in the span to lower-case.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
    /// <returns></returns>
    public static System.Span<char> ToLower(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceChar && char.ToLower(sourceChar, culture) is var targetChar && sourceChar != targetChar)
          source[index] = targetChar;

      return source;
    }
  }
}
