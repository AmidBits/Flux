namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the product of all numbers in <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSelf Product<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.Aggregate(TSelf.Zero, (a, e) => a * e);

    public static TSelf Product<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, System.Func<TSelf, TSelf> selector)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.Aggregate(TSelf.Zero, (a, e) => a * selector(e));
  }
}
