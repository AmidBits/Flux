namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TValue FastDigitCount<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TValue.One + value.FastIntegerLog(radix, UniversalRounding.WholeTowardZero, out var _);
  }
}
