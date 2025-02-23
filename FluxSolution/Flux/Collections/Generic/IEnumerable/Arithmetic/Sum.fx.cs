namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the sum of all numbers in <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSelf Sum<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.Aggregate(TSelf.Zero, (a, e) => a + e);
  }
}
