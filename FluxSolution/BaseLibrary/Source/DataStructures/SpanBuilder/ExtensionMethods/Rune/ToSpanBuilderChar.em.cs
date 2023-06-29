namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<char> ToSpanBuilderChar(this SpanBuilder<System.Text.Rune> source)
    {
      var target = new SpanBuilder<char>();

      for (var index = 0; index < source.Count; index++)
        target.Append(source[index].ToString(), 1);

      return target;
    }
  }
}
