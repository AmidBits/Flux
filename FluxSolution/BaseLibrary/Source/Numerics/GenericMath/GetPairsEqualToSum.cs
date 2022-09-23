#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetPairsEqualToSum<TSelf>(this System.Collections.Generic.IList<TSelf> self, TSelf sum)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      for (var i = 0; i < self.Count; i++)
      {
        var si = self[i];

        for (var j = i + 1; j < self.Count; j++)
        {
          var ti = self[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }
  }
}
#endif
