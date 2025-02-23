namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the integer (i.e. the truncated or floor) quotient and remainder of (<paramref name="dividend"/> / <paramref name="divisor"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns>Returns the integer (i.e. the truncated or floor) quotient and remainder.</returns>
    public static (TNumber TruncatedQuotient, TNumber Remainder) TruncRem<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var remainder = dividend % divisor;

      return ((dividend - remainder) / divisor, remainder);
    }
  }
}
