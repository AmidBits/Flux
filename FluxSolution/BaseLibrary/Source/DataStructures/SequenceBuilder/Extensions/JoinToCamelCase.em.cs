namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilderChar
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this SequenceBuilder<char> source, System.Func<char, bool> predicate)
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
    public static void JoinToCamelCase(this SequenceBuilder<char> source)
      => JoinToCamelCase(source, char.IsWhiteSpace);
  }

  public static partial class ExtensionMethodsSequenceBuilderRune
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this SequenceBuilder<System.Text.Rune> source, System.Func<System.Text.Rune, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = System.Text.Rune.ToUpperInvariant(source[index]);
        }
    }
    public static void JoinToCamelCase(this SequenceBuilder<System.Text.Rune> source)
      => JoinToCamelCase(source, System.Text.Rune.IsWhiteSpace);
  }
}
