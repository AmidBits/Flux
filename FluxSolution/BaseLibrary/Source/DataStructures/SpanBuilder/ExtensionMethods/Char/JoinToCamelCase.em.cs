namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this ref SpanBuilder<char> source, System.Func<char, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = 0; index < source.Length; index++)
        if (index == 0 || predicate(source[index]))
        {
          if (index == 0)
            source[index] = char.ToLower(source[index], culture);

          while (predicate(source[index]))
            source.Remove(index, 1);

          if (index > 0 && index < source.Length)
            source[index] = char.ToUpper(source[index], culture);
        }
    }
  }
}
