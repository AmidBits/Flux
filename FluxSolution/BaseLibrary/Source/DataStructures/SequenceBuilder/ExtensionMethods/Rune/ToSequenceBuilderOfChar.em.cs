namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<char> ToSequenceBuilderOfChar(this SequenceBuilder<System.Text.Rune> source)
    {
      var target = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString().AsSpan());

      return target;
    }
  }
}
