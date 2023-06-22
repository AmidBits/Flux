namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a new sequence of all pair indices for which values when added equals the specified sum.</summary>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetPairsEqualToSum<TSelf>(this System.Collections.Generic.IList<TSelf> collection, TSelf sum)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      for (var i = 0; i < collection.Count; i++)
      {
        var si = collection[i];

        for (var j = i + 1; j < collection.Count; j++)
        {
          var ti = collection[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }

#else

    /// <summary>Creates a new sequence of all pair indices for which values when added equals the specified sum.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger)> GetPairsEqualToSum(this System.Collections.Generic.IList<System.Numerics.BigInteger> collection, System.Numerics.BigInteger sum)
    {
      for (var i = 0; i < collection.Count; i++)
      {
        var si = collection[i];

        for (var j = i + 1; j < collection.Count; j++)
        {
          var ti = collection[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }

    /// <summary>Creates a new sequence of all pair indices for which values when added equals the specified sum.</summary>
    public static System.Collections.Generic.IEnumerable<(double, double)> GetPairsEqualToSum(this System.Collections.Generic.IList<double> collection, double sum)
    {
      for (var i = 0; i < collection.Count; i++)
      {
        var si = collection[i];

        for (var j = i + 1; j < collection.Count; j++)
        {
          var ti = collection[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }

#endif
  }
}
