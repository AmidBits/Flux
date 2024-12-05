namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a new sequence of all pair indices for which values when added equals the specified sum.</summary>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetPairsEqualToSum<TSelf>(this System.Collections.Generic.IList<TSelf> source, TSelf sum)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      for (var i = 0; i < source.Count; i++)
      {
        var si = source[i];

        for (var j = i + 1; j < source.Count; j++)
        {
          var ti = source[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }
  }
}
