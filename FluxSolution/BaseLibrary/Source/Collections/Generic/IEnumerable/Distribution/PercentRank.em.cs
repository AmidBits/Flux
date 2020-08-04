using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>The percent rank, in the range [0, 100], for the specified sequence. Percent rank is based on index, not values.</summary>
    /// <param name="source">A sequence of <typeparamref name="T"/>.</param>
    public static System.Collections.Generic.IList<double> PercentRank(this System.Collections.IEnumerable source)
    {
      var pr = new System.Collections.Generic.List<double>();

      var index = 0;

      foreach (var item in source)
      {
        pr.Add(++index - 0.5);
      }

      var multiplier = 100 / (double)pr.Count;

      while (--index >= 0)
      {
        pr[index] *= multiplier;
      }

      return pr;
    }
  }
}
