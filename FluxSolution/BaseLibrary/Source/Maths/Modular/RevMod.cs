namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Returns the remainder in reverse, i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range). The function also returns the normal modulo/remainder.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    public static TSelf RevMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      remainder = dividend % divisor;

      return TSelf.IsZero(remainder) ? remainder : -remainder + divisor;
    }

#endif
  }
}
