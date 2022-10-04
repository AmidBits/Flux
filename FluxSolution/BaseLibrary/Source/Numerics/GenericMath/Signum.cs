#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the sign of <paramref name="x"/>.</summary>
    public static int Signum<TSignedValue>(this TSignedValue x)
      where TSignedValue : System.Numerics.ISignedNumber<TSignedValue>
      => TSignedValue.IsNegative(x) ? -1 : TSignedValue.IsPositive(x) ? 1 : 0;
  }
}
#endif
