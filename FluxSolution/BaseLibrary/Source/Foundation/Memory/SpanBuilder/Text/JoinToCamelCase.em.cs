namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(ref this SpanBuilder<char> source, System.Func<char, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = char.ToUpper(source[index]);
        }
    }
    public static void JoinToCamelCase(ref this SpanBuilder<char> source)
      => JoinToCamelCase(ref source, char.IsWhiteSpace);
  }
}
