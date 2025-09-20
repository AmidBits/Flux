namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => GreatestCommonDivisor(a, b) == TInteger.One;
  }
}
