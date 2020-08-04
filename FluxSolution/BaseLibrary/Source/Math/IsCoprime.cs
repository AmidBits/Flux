namespace Flux
{
  public static partial class Math
  {
    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => System.Numerics.BigInteger.GreatestCommonDivisor(a, b) == 1;

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(int a, int b)
      => GreatestCommonDivisor(a, b) == 1;
    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(long a, long b)
      => GreatestCommonDivisor(a, b) == 1;
  }
}
