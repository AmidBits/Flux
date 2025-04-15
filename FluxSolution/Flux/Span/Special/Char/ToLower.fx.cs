namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Convert the entire span to lower-case. Uses the specified <paramref name="culture"/>, or current-culture if null.</para>
    /// </summary>
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
