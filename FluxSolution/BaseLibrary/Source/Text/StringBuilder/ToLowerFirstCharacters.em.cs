namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Makes any upper case character with a whitespace on the left and a lower case character on the right into a lower case character.</summary>
    public static System.Text.StringBuilder ToLowerFirstCharacters(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = source.Length - 1; index >= 0; index--)
        if (char.IsUpper(source[index]) && (index == 0 || char.IsWhiteSpace(source[index - 1])) && char.IsLower(source[index + 1]))
          source[index] = char.ToLowerInvariant(source[index]);

      return source;
    }
  }
}
