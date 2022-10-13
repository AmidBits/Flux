#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="x"/> as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Numerics.BigInteger.CreateChecked(x);

    /// <summary>Returns the value of <paramref name="x"/> as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf x, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => System.Numerics.BigInteger.CreateChecked(Rounding<TSelf>.Round(x, mode));
  }
}
#endif
