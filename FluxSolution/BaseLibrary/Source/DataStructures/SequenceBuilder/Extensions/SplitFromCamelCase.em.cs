namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilderChar
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase(this SequenceBuilder<char> source, char separator)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, separator);
    }
  }

  public static partial class ExtensionMethodsSequenceBuilderRune
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase(this SequenceBuilder<System.Text.Rune> source, System.Text.Rune separator)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (System.Text.Rune.IsUpper(source[index]) && (!System.Text.Rune.IsUpper(source[index - 1]) || System.Text.Rune.IsLower(source[index + 1])))
          source.Insert(index, separator);
    }
  }
}
