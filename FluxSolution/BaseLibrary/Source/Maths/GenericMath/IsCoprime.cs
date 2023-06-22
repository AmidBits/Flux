namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GreatestCommonDivisor(a, b) == TSelf.One;

#else

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(this System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => System.Numerics.BigInteger.GreatestCommonDivisor(a, b) == 1;

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(this int a, int b)
      => GreatestCommonDivisor(a, b) == 1;

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime(this long a, long b)
      => GreatestCommonDivisor(a, b) == 1;

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    [System.CLSCompliant(false)]
    public static bool IsCoprime(this uint a, uint b)
      => GreatestCommonDivisor(a, b) == 1;

    /// <summary>Determines whether the two numbers are coprime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Coprime_integers"/>
    [System.CLSCompliant(false)]
    public static bool IsCoprime(this ulong a, ulong b)
      => GreatestCommonDivisor(a, b) == 1;

#endif
  }
}
