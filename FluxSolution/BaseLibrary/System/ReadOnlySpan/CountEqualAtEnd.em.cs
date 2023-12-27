namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer (null for default).</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      var count = 0;

      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
        count++;

      return count;
    }
  }
}
