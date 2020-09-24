namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<T> ToSpan<T>(this System.Span<T> source)
      => source.ToArray();

    /// <summary>Creates a string builder from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.Span<char> source)
      => new System.Text.StringBuilder(new string(source.ToArray()));
  }
}
