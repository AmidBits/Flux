using System.Linq;

namespace Flux
{
  public static partial class Reflection
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult median, out System.Collections.Generic.List<TSelf> sortedList)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var (quotient, remainder) = int.DivRem(sortedList.Count, 2);

      median = (remainder == 0)
        ? TResult.CreateChecked(sortedList.ElementAt(quotient - 1) + sortedList.ElementAt(quotient)).Divide(2) // Even count = ((previous + current) / 2).
        : TResult.CreateChecked(sortedList.ElementAt(quotient)); // Odd count = current value only.
    }

#else

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median(this System.Collections.Generic.IEnumerable<decimal> source, out decimal median, out System.Collections.Generic.List<decimal> sortedList)
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var quotient = System.Math.DivRem(sortedList.Count, 2, out var remainder);

      median = (remainder == 0)
        ? (sortedList.ElementAt(quotient - 1) + sortedList.ElementAt(quotient)) / 2 // Even count = ((previous + current) / 2).
        : sortedList.ElementAt(quotient); // Odd count = current value only.
    }

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median(this System.Collections.Generic.IEnumerable<double> source, out double median, out System.Collections.Generic.List<double> sortedList)
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var quotient = System.Math.DivRem(sortedList.Count, 2, out var remainder);

      median = (remainder == 0)
        ? (sortedList.ElementAt(quotient - 1) + sortedList.ElementAt(quotient)) / 2 // Even count = ((previous + current) / 2).
        : sortedList.ElementAt(quotient); // Odd count = current value only.
    }

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source, out double median, out System.Collections.Generic.List<System.Numerics.BigInteger> sortedList)
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var quotient = System.Numerics.BigInteger.DivRem(sortedList.Count, 2, out var remainder);

      median = remainder.IsZero
        ? (double)(sortedList.ElementAt((int)quotient - 1) + sortedList.ElementAt((int)quotient)) / 2 // Even count = ((previous + current) / 2).
        : (double)sortedList.ElementAt((int)quotient); // Odd count = current value only.
    }

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median(this System.Collections.Generic.IEnumerable<int> source, out double median, out System.Collections.Generic.List<int> sortedList)
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var quotient = System.Math.DivRem(sortedList.Count, 2, out var remainder);

      median = (remainder == 0)
        ? (double)(sortedList.ElementAt(quotient - 1) + sortedList.ElementAt(quotient)) / 2 // Even count = ((previous + current) / 2).
        : sortedList.ElementAt(quotient); // Odd count = current value only.
    }

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <see href="http://en.wikipedia.org/wiki/Median"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Median(this System.Collections.Generic.IEnumerable<long> source, out double median, out System.Collections.Generic.List<long> sortedList)
    {
      sortedList = source.ToList();
      sortedList.Sort();

      var quotient = System.Math.DivRem(sortedList.Count, 2, out var remainder);

      median = (remainder == 0)
        ? (double)(sortedList.ElementAt(quotient - 1) + sortedList.ElementAt(quotient)) / 2 // Even count = ((previous + current) / 2).
        : sortedList.ElementAt(quotient); // Odd count = current value only.
    }

#endif
  }
}
