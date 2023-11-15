namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<char> ToSpanBuilderOfChar(this SpanBuilder<System.Text.Rune> source)
    {
      var target = new SpanBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString(), 1);

      return target;
    }
  }
}
