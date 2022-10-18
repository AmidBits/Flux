#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Creates a new sequence of all pairs that equals the specified sum.</summary>
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
  }
}
#endif
