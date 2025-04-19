namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Compute the mean, median and mode of all elements in <paramref name="source"/>. This version uses a <see cref="Statistics.OnlineMeanMedianMode{TSelf}"/> with double heaps and a histogram.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Median"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    public static (double Mean, double Median, System.Collections.Generic.KeyValuePair<TSelf, int> Mode) MeanMedianMode<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var mmo = new Statistics.OnlineMeanMedianMode<TSelf>(source);

      return (mmo.Mean, mmo.Median, mmo.Mode);
    }
  }
}
