namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Copy the specified count from source into target at the specified offset. If the count wraps the target, it will be wrapped to the beginning in a circular fashion.</summary>
    public static void CircularCopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset, int count)
    {
      for (var index = 0; index < count; index++)
        target[(offset + index) % target.Length] = source[index % source.Length];
    }
  }
}
