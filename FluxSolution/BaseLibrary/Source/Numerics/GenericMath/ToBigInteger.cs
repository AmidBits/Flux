namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="x"/> (of type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Numerics.BigInteger.CreateChecked(x);

    /// <summary>Returns the value of <paramref name="x"/> (of type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/> rounded using <see cref="RoundingMode"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf x, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => System.Numerics.BigInteger.CreateChecked(Rounding<TSelf>.Round(x, mode));
  }
}
