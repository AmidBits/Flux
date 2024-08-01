namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the <paramref name="value"/> (of integer type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Numerics.BigInteger.CreateChecked(value);
  }
}
