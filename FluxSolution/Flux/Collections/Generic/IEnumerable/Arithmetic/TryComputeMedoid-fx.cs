namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Compute the medoid of <paramref name="source"/>, also return the <paramref name="index"/> and the <paramref name="count"/> of elements as output parameters.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Medoid"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
    public static bool TryComputeMedoid<TSelf>(this System.Collections.Generic.IList<TSelf> source, out TSelf result)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IMinMaxValue<TSelf>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      result = default!;

      var minTotalDistance = TSelf.MaxValue;

      var hasResult = false;

      foreach (TSelf candidate in source)
        if (source.Sum(t => candidate - t) is var candidateTotalDistance && candidateTotalDistance < minTotalDistance)
        {
          result = candidate;

          minTotalDistance = candidateTotalDistance;

          hasResult = true;
        }

      return hasResult;
    }
  }
}
