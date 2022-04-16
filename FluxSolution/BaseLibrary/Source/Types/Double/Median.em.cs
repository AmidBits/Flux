using System.Linq;

namespace Flux
{
  public static partial class DoubleEm
  {
    /// <summary>Calculate the median of a sequence. This implementation requires an ordered enumerable and enumerates the sequence multiple times.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Linq.IOrderedEnumerable<double> source)
      => System.Math.DivRem(source.Count(), 2, out int remainder) is var quotient && remainder == 0 ? source.Skip(quotient - 1).Take(2).Average() : source.ElementAt(quotient);
    /// <summary>Calculate the median of a sequence. This implementation enumerates the sequence multiple times.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Collections.Generic.IEnumerable<double> source)
      => Median(System.Linq.Enumerable.OrderBy(source, v => v));

    /// <summary>Calculate the median of the IList. This list must be pre-sorted.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    private static double Median(this System.Collections.Generic.IList<double> source)
      => System.Math.DivRem(source.Count, 2, out int remainder) is var quotient && remainder == 0 ? (source[quotient - 1] + source[quotient]) / 2 : source[quotient];
    /// <summary>Calculate the median of a sequence. This implementation requires an ordered enumerable and buffers the sequence to avoid multiple passes.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Linq.IOrderedEnumerable<double> source, out System.Collections.Generic.List<double> orderedList)
      => Median(orderedList = new System.Collections.Generic.List<double>(source));
    /// <summary>Calculate the median of a sequence. This version buffers the sequence to avoid multiple passes.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Median"/>
    public static double Median(this System.Collections.Generic.IEnumerable<double> source, out System.Collections.Generic.List<double> orderedList)
    {
      orderedList = new System.Collections.Generic.List<double>(source);
      orderedList.Sort();
      return Median(orderedList);
    }
  }
}
