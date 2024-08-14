namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<char> ToUpperCase(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceChar && char.ToUpper(sourceChar, culture) is var targetChar && sourceChar != targetChar)
          source[index] = targetChar;

      return source;
    }
  }
}
