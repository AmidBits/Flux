namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Span{T}"/> with a copy of the <paramref name="source"/> string.</summary>
    public static System.Span<char> ToSpan(this string source) => new System.Span<char>(source.ToCharArray());
  }
}
