namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> % <paramref name="divisor"/>) as an output parameter.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>Returns the integer (truncated or floor) quotient and the remainder as an out parameter.</returns>
    public static TNumber TruncRem<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }

    public static TNumber TruncRem<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder, out int remainderSign)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainderSign = TNumber.Sign(remainder = dividend % divisor);

      return (dividend - remainder) / divisor;
    }
  }
}
