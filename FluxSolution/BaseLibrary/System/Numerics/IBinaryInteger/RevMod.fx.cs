namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the <paramref name="remainder"/> in reverse, i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range). The function also returns the normal <paramref name="remainder"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    public static TValue RevMod<TValue>(this TValue dividend, TValue divisor, out TValue remainder)
      where TValue : System.Numerics.INumber<TValue>
    {
      remainder = dividend % divisor;

      return TValue.IsZero(remainder) ? remainder : TValue.CopySign(divisor, remainder) - remainder;
    }
  }
}
