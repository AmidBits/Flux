namespace Flux
{
  public static partial class GenericMathFast
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TIRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TINumber FastDigitCount<TINumber, TIRadix>(this TINumber value, TIRadix radix)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TIRadix : System.Numerics.IBinaryInteger<TIRadix>
      => TINumber.One + value.FastIntegerLog(radix, UniversalRounding.WholeTowardZero, out var _);
  }
}
