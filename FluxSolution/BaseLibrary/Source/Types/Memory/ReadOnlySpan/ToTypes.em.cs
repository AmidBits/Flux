namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a Span from the source.</summary>
    public static System.Span<T> ToSpan<T>(this System.ReadOnlySpan<T> source)
      => source.ToArray();

    /// <summary>Creates a string builder from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source)
      => new System.Text.StringBuilder().Append(source);
  }
}
