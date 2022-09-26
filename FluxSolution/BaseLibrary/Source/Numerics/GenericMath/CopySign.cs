#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the <see cref="System.Numerics.INumber{TSelf}">value</see> as the same type with the sign of any <see cref="System.Numerics.ISignedNumber{TSign}">sign</see>.</summary>
    public static TResult CopySign<TSelf, TSign, TResult>(this TSelf value, TSign sign, out TResult result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TSign : System.Numerics.ISignedNumber<TSign>
      where TResult : System.Numerics.INumber<TResult>
      => result = TResult.CreateChecked(TSign.IsNegative(sign) ? -TSelf.Abs(value) : TSelf.Abs(value));
  }
}
#endif
