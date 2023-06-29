namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static string ToString<T>(this SpanBuilder<T> source, int startIndex, int count)
    {
      var sb = new System.Text.StringBuilder();

      while (count-- > 0)
        sb.Append($"{source[startIndex++]}");

      return sb.ToString();
    }

    public static string ToString<T>(this SpanBuilder<T> source, int startIndex) => ToString(source, startIndex, source.Count);
  }
}
