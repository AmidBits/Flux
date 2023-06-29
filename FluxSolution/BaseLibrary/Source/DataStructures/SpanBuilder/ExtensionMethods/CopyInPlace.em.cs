namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<T> CopyInPlace<T>(this SpanBuilder<T> source, int fromIndex, int toIndex, int count)
    {
      if (fromIndex < 0 || fromIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (toIndex < 0 || toIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (count < 0 || fromIndex + count > source.Count || toIndex + count > source.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      while (count-- > 0)
        source[toIndex++] = source[fromIndex++];

      return source;
    }
  }
}
