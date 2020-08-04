namespace Flux
{
  public static partial class Math
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNearestPrimes(System.Numerics.BigInteger number, int count)
    {
      foreach (var ppn in GetNearestPotentialPrimes(number))
      {
        if (IsPrimeNumber(ppn))
        {
          yield return ppn;

          if (--count <= 0) yield break;
        }
      }
    }

    public static System.Collections.Generic.IEnumerable<int> GetNearestPrimes(int number, int count)
    {
      foreach (var ppn in GetNearestPotentialPrimes(number))
      {
        if (IsPrimeNumber(ppn))
        {
          yield return ppn;

          if (--count <= 0) yield break;
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<long> GetNearestPrimes(long number, int count)
    {
      foreach (var ppn in GetNearestPotentialPrimes(number))
      {
        if (IsPrimeNumber(ppn))
        {
          yield return ppn;

          if (--count <= 0) yield break;
        }
      }
    }
  }
}
