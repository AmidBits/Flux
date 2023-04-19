using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="orderedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult median, out System.Collections.Generic.List<TSelf> orderedList)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      orderedList = source.ToList();
      orderedList.Sort();

      var (quotient, remainder) = int.DivRem(orderedList.Count, 2);

      var value = orderedList.ElementAt(quotient);

      if (remainder == 0)
      {
        value += orderedList.ElementAt(quotient - 1); // We use a second element.

        median = TResult.CreateChecked(value).Divide(2); // Compute the average of the two.
      }
      else
        median = TResult.CreateChecked(value); // Only one element used.
    }

#endif
  }
}
