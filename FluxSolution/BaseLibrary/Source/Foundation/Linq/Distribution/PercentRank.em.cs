using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    public static double PercentRank<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, int index)
      => Maths.PercentRank(source.Count(), index);

    /// <summary>The percent rank, in the range [0, 100], for the specified number of items in the sequence. Percent rank is based on index, not values.</summary>
    public static System.Collections.Generic.IEnumerable<double> PercentRank<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
      => Maths.PercentRank(source.Count());
  }
}
