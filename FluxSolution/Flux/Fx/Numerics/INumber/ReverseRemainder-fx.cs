namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>
    /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range).</para>
    /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    /// </returns>
    public static TNumber ReverseRemainder<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsZero(remainder = dividend % divisor) ? TNumber.Zero : TNumber.CopySign(divisor, remainder) - remainder;
  }
}
