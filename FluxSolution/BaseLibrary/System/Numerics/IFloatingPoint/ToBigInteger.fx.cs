namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the <paramref name="value"/> (of floating-point type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/> using the rounding <paramref name="mode"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf value, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => System.Numerics.BigInteger.CreateChecked(value.Round(mode));
  }
}
