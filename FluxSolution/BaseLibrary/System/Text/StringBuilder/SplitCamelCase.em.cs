namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static System.Text.StringBuilder SplitCamelCase(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, ' ');

      return source;
    }
  }
}