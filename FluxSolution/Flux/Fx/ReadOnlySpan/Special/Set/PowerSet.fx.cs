namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Power_set"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.List<T[]> PowerSet<T>(this System.ReadOnlySpan<T> source)
    {
      var powerSet = new System.Collections.Generic.List<T[]>();

      var powerCount = (int)System.Numerics.BigInteger.Pow(2, source.Length);

      var subsetList = new System.Collections.Generic.List<T>(source.Length);

      for (var o = 0; o < powerCount; o++)
      {
        subsetList.Clear();

        for (var i = 0; i < powerCount; i++)
          if ((o & (1L << i)) > 0)
            subsetList.Add(source[i]);

        powerSet.Add(subsetList.ToArray());
      }

      return powerSet;
    }
  }
}
