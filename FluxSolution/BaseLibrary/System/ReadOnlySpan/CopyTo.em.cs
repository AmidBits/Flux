namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Copy the specified <paramref name="count"/> of elements from <paramref name="source"/> into <paramref name="target"/> at the specified <paramref name="offset"/>.</summary>
    public static void CopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset, int count)
    {
      for (count--; count >= 0; count--)
        target[offset + count] = source[count];
    }
    /// <summary>Copy all <paramref name="source"/> elements into <paramref name="target"/> at the specified <paramref name="offset"/>.</summary>
    public static void CopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset)
      => CopyTo(source, target, offset, source.Length);
  }
}
