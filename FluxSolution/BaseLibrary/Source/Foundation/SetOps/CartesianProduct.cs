namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements as the set of all ordered pairs (s, t) where s is in source and t is in target.</summary>
    public static System.Collections.Generic.IEnumerable<(T1, T2)> CartesianProduct<T1, T2>(this System.Collections.Generic.IEnumerable<T1> source, System.Collections.Generic.IEnumerable<T2> target)
    {
      using var eSource = source.GetEnumerator();

      while (eSource.MoveNext())
      {
        using var eTarget = target.GetEnumerator();

        while (eTarget.MoveNext())
          yield return (eSource.Current, eTarget.Current);
      }
    }
  }
}
