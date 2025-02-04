namespace Flux
{
  public static partial class Fx
  {
    public static TValue FastDigitCount<TValue, TRadix>(this TValue value, TRadix radix, out System.Numerics.BigInteger pow)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var v = System.Numerics.BigInteger.CreateChecked(value);
      var r = System.Numerics.BigInteger.CreateChecked(radix);

      var fil = v.FastIntegerLog(r, UniversalRounding.WholeAwayFromZero, out var _);

      pow = r.FastIntegerPow(fil, UniversalRounding.HalfTowardZero, out var _);

      return TValue.CreateChecked(v == pow ? fil + System.Numerics.BigInteger.One : fil);
    }
  }
}
