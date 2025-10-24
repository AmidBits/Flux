namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Compute the medoid of <paramref name="source"/> and return the result as an output parameter.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Medoid"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="medoid"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
    public static bool TryComputeMedoid<TNumber>(this System.Collections.Generic.IList<TNumber> source, out TNumber medoid)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IMinMaxValue<TNumber>
    {
      medoid = default!;

      var minTotalDistance = TNumber.MaxValue;

      var hasResult = false;

      foreach (TNumber candidate in source)
        if (source.Sum(t => candidate - t) is var candidateTotalDistance && candidateTotalDistance < minTotalDistance)
        {
          medoid = candidate;

          minTotalDistance = candidateTotalDistance;

          hasResult = true;
        }

      return hasResult;
    }
  }
}
