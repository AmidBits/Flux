namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static int CommonPrefixLength<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Zip(target).TakeWhile((e, i) => equalityComparer.Equals(e.First, e.Second) && i < maxLength).Count();
    }
  }
}
