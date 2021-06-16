using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Calculate the mean of a sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mode(this System.Collections.Generic.IEnumerable<double> source)
      => source.GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key;
  }
}
