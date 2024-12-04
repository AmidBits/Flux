namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>
    /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range).</para>
    /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    /// </returns>
    public static TValue RevRem<TValue>(this TValue dividend, TValue divisor, out TValue remainder)
      where TValue : System.Numerics.INumber<TValue>
    {
      remainder = dividend % divisor;

      return TValue.IsZero(remainder) ? remainder : TValue.CopySign(divisor, remainder) - remainder;
    }
  }
}
