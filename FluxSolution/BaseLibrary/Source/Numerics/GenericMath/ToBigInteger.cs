namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="x"/> (of type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => ToType(x, out System.Numerics.BigInteger _);
  }
}
