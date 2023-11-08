namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      var target = new SpanBuilder<T>();

      foreach (var item in source)
        target.Add(item);

      return target;
    }
  }
}
