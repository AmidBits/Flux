using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Computes the percent rank, in the range [0, 100], for the specified ordinal index within the number of elements.</summary>
    public static double PercentRank(int index, int count, double factor = 100)
      => index < 1 ? 0 : index > count ? 100 : (index - 0.5) / count * factor;

    /// <summary>Computes the percent ranks, range [0, 100], for the specified number of elements.</summary>
    public static System.Collections.Generic.IList<double> PercentRanks(int count, double factor = 100)
      => System.Linq.Enumerable.Range(1, count).Select(index => PercentRank(index, count, factor)).ToList();
  }
}
