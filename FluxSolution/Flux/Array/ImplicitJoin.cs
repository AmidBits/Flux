namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Perform an inner join of all sub-sequences in <paramref name="source"/>.</para>
    /// </summary>
    /// <returns></returns>
    public static System.Collections.Generic.HashSet<T> ImplicitJoin<T>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var e = source.GetEnumerator();

      var ij = new System.Collections.Generic.HashSet<T>();

      if (e.MoveNext())
      {
        ij = new(e.Current);

        while (e.MoveNext())
          ij = new(ij.Join(e.Current, outer => outer, inner => inner, (outer, inner) => inner));
      }

      return ij;
    }
  }
}
