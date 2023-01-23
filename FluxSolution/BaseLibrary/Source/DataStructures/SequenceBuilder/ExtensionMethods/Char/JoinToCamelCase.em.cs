namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static SequenceBuilder<char> JoinToCamelCase(this SequenceBuilder<char> source, System.Func<char, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = char.ToUpper(source[index]);
        }

      return source;
    }
    public static SequenceBuilder<char> JoinToCamelCase(this SequenceBuilder<char> source)
      => JoinToCamelCase(source, char.IsWhiteSpace);
  }
}
