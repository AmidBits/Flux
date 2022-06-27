namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Copy the specified count from source into target at the specified offset.</summary>
    public static void CopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset, int count)
    {
      for (count--; count >= 0; count--)
        target[offset + count] = source[count];
    }
    /// <summary>Copy the source into target at the specified offset.</summary>
    public static void CopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset)
      => CopyTo(source, target, offset, source.Length);
    /// <summary>Copy the source into target at the specified offset.</summary>
    public static void CopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target)
      => CopyTo(source, target, 0, source.Length);
  }
}
