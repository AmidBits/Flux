namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the <paramref name="value"/> (of integer type <typeparamref name="TValue"/>) as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => System.Numerics.BigInteger.CreateChecked(value);
  }
}
