namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime<TValue>(this TValue a, TValue b)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => GreatestCommonDivisor(a, b) == TValue.One;
  }
}
