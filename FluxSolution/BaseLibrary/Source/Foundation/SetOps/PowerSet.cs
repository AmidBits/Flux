namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Power_set"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<T>> PowerSet<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      var hs = System.Linq.Enumerable.ToList(source);

      var powerCount = (int)System.Math.Pow(2, hs.Count);

      for (var outer = 0; outer < powerCount; outer++)
      {
        var list = new System.Collections.Generic.List<T>();

        for (var inner = 0; inner < powerCount; inner++)
          if ((outer & (System.Numerics.BigInteger.One << inner)) > 0)
            list.Add(hs[inner]);

        yield return list;
      }
    }
  }
}
