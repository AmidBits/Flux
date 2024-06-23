namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the count of elements equal at the end of the sequences. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var count = 0;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
        count++;

      return count;
    }
  }
}
