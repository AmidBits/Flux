namespace Flux
{
  public static partial class Fx
  {
    public static TValue FastDigitCount<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var fil = value.FastIntegerLog(radix, UniversalRounding.WholeAwayFromZero, out var _);

      var fip = TValue.CreateChecked(radix).FastIntegerPow(fil, UniversalRounding.HalfTowardZero, out var _);

      return (value == fip) ? fil + TValue.One : fil;
    }
  }
}
