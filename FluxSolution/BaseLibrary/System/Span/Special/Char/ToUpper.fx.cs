namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Convert the entire span to upper-case. Uses the specified <paramref name="culture"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Span<char> ToUpper(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceChar && char.ToUpper(sourceChar, culture) is var targetChar && sourceChar != targetChar)
          source[index] = targetChar;

      return source;
    }
  }
}
