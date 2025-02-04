namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Median"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="median"></param>
    /// <param name="sortedList"></param>
    public static void Median<TSelf, TResult>(this System.Collections.Generic.IList<TSelf> source, out TResult median)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var (quotient, remainder) = int.DivRem(source.Count, 2);

      median = (remainder == 0)
        ? TResult.CreateChecked(source[quotient - 1] + source[quotient]) / TResult.CreateChecked(2) // Even count = ((previous + current) / 2).
        : TResult.CreateChecked(source[quotient]); // Odd count = current value only.
    }
  }
}
