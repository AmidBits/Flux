using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsISetT
  {
    /// <summary>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see cref="https://en.wikipedia.org/wiki/Power_set"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<T>> PowerSet<T>(this System.Collections.Generic.IList<T> source)
    {
      var powerCount = (int)System.Numerics.BigInteger.Pow(2, source.Count);

      var subsetList = new System.Collections.Generic.List<T>(source.Count);

      for (var o = 0; o < powerCount; o++)
      {
        subsetList.Clear();

        for (var i = 0; i < powerCount; i++)
          if ((o & (1L << i)) > 0)
            subsetList.Add(source[i]);

        yield return subsetList.ToArray();
      }
    }

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<T>> PowerSet<T>(this System.Collections.Generic.ISet<T> source)
      => source.ToList().PowerSet();
  }
}
