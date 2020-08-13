namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Calculates the power of 2 and the specified exponent.</summary>
    public static System.Numerics.BigInteger Pow2(int exponent)
      => exponent >= 0 ? System.Numerics.BigInteger.One << exponent : throw new System.ArgumentOutOfRangeException(nameof(exponent));
  }
}
