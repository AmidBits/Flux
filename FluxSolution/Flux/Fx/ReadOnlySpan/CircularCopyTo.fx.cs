namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Copies the specified <paramref name="count"/> from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> at the specified <paramref name="targetIndex"/>. If the <paramref name="count"/> wraps the <paramref name="target"/>, it will be wrapped to the beginning in a circular fashion. The <paramref name="source"/> is treated the same way.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="target"></param>
    /// <param name="targetIndex"></param>
    /// <param name="count"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static void CircularCopyTo<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.Span<T> target, int targetIndex, int count)
    {
      if (sourceIndex < 0 || sourceIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
      if (targetIndex < 0 || targetIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (var index = 0; index < count; index++)
        target[(targetIndex + index) % target.Length] = source[(sourceIndex + index) % source.Length];
    }
  }
}
