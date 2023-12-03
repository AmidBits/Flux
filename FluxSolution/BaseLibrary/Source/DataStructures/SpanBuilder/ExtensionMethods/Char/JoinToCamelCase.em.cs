namespace Flux
{
  public static partial class Em
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanBuilder<char> JoinToCamelCase(ref this SpanBuilder<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = 0; index < source.Length; index++)
        if (index == 0 || source[index] == separator)
        {
          if (index == 0)
            source[index] = char.ToLower(source[index], culture);

          while (source[index] == separator)
            source.Remove(index, 1);

          if (index > 0 && index < source.Length)
            source[index] = char.ToUpper(source[index], culture);
        }

      return source;
    }
  }
}
