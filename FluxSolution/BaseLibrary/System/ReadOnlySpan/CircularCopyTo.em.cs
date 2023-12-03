namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Copies the specified <paramref name="count"/> from <paramref name="source"/> into <paramref name="target"/> at the specified <paramref name="offset"/>. If the <paramref name="count"/> wraps the <paramref name="target"/>, it will be wrapped to the beginning in a circular fashion. The <paramref name="source"/> is treated the same way.</summary>
    public static void CircularCopyTo<T>(this System.ReadOnlySpan<T> source, System.Span<T> target, int offset, int count)
    {
      for (var index = 0; index < count; index++)
        target[(offset + index) % target.Length] = source[index % source.Length];
    }
  }
}
