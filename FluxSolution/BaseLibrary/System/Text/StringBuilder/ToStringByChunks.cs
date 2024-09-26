namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Kept this primarily to show how string.Create<> can be used.</para>
    /// <para><see href="https://stackoverflow.com/a/54598107/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToStringByChunks(this System.Text.StringBuilder source)
      => string.Create(source.Length, source, (span, sb) =>
      {
        foreach (var chunk in sb.GetChunks())
        {
          chunk.Span.CopyTo(span);

          span = span.Slice(chunk.Length);
        }
      });
  }
}
