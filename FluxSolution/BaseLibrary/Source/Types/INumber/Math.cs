namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the sign of the value.
    /// -1, x < 0
    ///  1, x > 0
    ///  0, x = 0
    /// </summary>
    public static TSelf Sign<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ISignedNumber<TSelf>
      => value < TSelf.Zero ? TSelf.NegativeOne : value > TSelf.Zero ? TSelf.One : TSelf.Zero;

    /// <summary>PREVIEW! Returns the absolute <paramref name="value"/> with the sign of <paramref name="sign"/>.</summary>
    public static TSelf CopySign<TSelf>(this TSelf value, TSelf sign)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ISignedNumber<TSelf>
      => TSelf.Abs(value) * sign.Sign();
  }
}
