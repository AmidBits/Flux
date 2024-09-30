namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    /// <remarks>
    /// <para>There is a built-in method for <see cref="System.MemoryExtensions.CommonPrefixLength{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, IEqualityComparer{T}?)"/> but not one for common-suffix-length.</para>
    /// </remarks>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var length = 0;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
        length++;

      return length;
    }
  }
}
