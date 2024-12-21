namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static int CommonPrefixLength<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Zip(target).Count(z => equalityComparer.Equals(z.First, z.Second));
    }
  }
}
