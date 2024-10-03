namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the specified part of the <paramref name="target"/> is found at the specified <paramref name="targetIndex"/> in the <paramref name="source"/> at <paramref name="sourceIndex"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="target"></param>
    /// <param name="targetIndex"></param>
    /// <param name="length"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      //if (sourceIndex < 0 || sourceIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
      //if (targetIndex < 0 || targetIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));

      //if (length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length) throw new System.ArgumentOutOfRangeException(nameof(length));

      if (sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length)
        return false;

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      while (length-- > 0)
        if (!equalityComparer.Equals(source[sourceIndex++], target[targetIndex++]))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Indicates whether the specified target is found at the specified index in the source, using the specified comparer.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="target"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
      => EqualsAt(source, sourceIndex, target, 0, System.Math.Min(source.Length - sourceIndex, target.Length), comparer);
  }
}
