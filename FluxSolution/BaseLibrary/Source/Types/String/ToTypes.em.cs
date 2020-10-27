namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Forms a new ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<char> ToReadOnlySpan(this string source)
      => source?.ToCharArray() ?? throw new System.ArgumentNullException(nameof(source));

    /// <summary>Forms a new Span from the source.</summary>
    public static System.Span<char> ToSpan(this string source)
      => source?.ToCharArray() ?? throw new System.ArgumentNullException(nameof(source));

    public static System.Text.StringBuilder ToStringBuilder(this string source)
      => new System.Text.StringBuilder(source);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length)
      => new System.Text.StringBuilder(source, startIndex, length, length);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length, int capacity)
      => new System.Text.StringBuilder(source, startIndex, length, capacity);
  }
}
