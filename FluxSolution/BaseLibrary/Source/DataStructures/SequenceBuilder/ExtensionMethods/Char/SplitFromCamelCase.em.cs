namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SequenceBuilder<char> SplitFromCamelCase(this SequenceBuilder<char> source, char separator)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, separator);

      return source;
    }
  }
}
