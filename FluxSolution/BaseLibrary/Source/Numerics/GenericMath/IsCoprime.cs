namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    public static bool IsCoprime<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GreatestCommonDivisor(a, b) == TSelf.One;
  }
}
