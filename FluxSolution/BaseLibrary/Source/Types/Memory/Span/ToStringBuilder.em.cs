namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Creates a string builder from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.Span<char> source)
      => new System.Text.StringBuilder().Append(source);
  }
}
