using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>
    /// <para>Calculate the median of a sequence. This version buffers the sequence to avoid multiple passes.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static TSelf Median<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, out System.Collections.Generic.List<TSelf> orderedList)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      orderedList = source.ToList();
      orderedList.Sort();

      return System.Math.DivRem(orderedList.Count(), 2, out int remainder) is var quotient && remainder == 0 ? (orderedList.ElementAt(quotient - 1) + orderedList.ElementAt(quotient)).Divide(2) : orderedList.ElementAt(quotient);
    }
  }
}
