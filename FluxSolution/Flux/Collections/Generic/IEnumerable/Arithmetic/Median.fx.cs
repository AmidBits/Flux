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
    public static void Median<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult median, out System.Collections.Generic.List<TSelf> sortedList)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      sortedList = source.ToList();
      sortedList.Sort();
      sortedList.Median(out median);
    }
  }
}
