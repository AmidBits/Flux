namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the characters in source.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<char> ToSpanMaker(this System.Text.StringBuilder source)
    {
      var sm = new SpanMaker<char>(source.Length);
      foreach (var chunk in source.GetChunks())
        sm.Append(1, chunk.Span);
      return sm;
    }
  }
}
