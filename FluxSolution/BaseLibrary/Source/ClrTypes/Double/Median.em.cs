using System.Linq;

namespace Flux
{
  public static partial class XtensionsDouble
  {
    /// <summary>Calculate the median of a sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Collections.Generic.IEnumerable<double> source)
      => source.OrderBy(v => v).Median();

    /// <summary>Calculate the median of a sequence. This implementation requires an ordered enumerable and enumerates the sequence multiple times.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Linq.IOrderedEnumerable<double> source)
      => System.Math.DivRem(source.Count(), 2, out int remainder) is var quotient && remainder == 0 ? source.Skip(quotient - 1).Take(2).Average() : source.ElementAt(quotient);
  }
}
