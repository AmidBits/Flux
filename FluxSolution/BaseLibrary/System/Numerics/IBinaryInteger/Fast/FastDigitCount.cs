namespace Flux
{
  public static partial class Fx
  {
    public static TValue FastDigitCount<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value.FastIntegerLog(radix, UniversalRounding.WholeAwayFromZero, out var _);
  }
}
