namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Forms a new ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<char> ToReadOnlySpan(this string source)
      => source?.ToCharArray() ?? throw new System.ArgumentNullException(nameof(source));

    /// <summary>Forms a new Span from the source.</summary>
    public static System.Span<char> ToSpan(this string source)
      => source?.ToCharArray() ?? throw new System.ArgumentNullException(nameof(source));
  }
}
