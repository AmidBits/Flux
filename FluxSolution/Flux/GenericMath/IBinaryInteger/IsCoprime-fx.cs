namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => GreatestCommonDivisor(a, b) == TNumber.One;
  }
}
