namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the <paramref name="value"/> (of floating-point type <typeparamref name="TValue"/>) as a <see cref="System.Numerics.BigInteger"/> using the rounding <paramref name="mode"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TValue>(this TValue value, RoundingMode mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => System.Numerics.BigInteger.CreateChecked(value.Round(mode));
  }
}
