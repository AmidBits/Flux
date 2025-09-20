namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TIRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TINumber FastDigitCount<TINumber, TIRadix>(this TINumber value, TIRadix radix)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TIRadix : System.Numerics.IBinaryInteger<TIRadix>
      => TINumber.One + value.FastIntegerLog(radix, MidpointRounding.ToZero, out var _);
  }
}
