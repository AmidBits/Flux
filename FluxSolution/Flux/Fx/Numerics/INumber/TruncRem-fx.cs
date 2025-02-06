namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the integer (i.e. truncated/floor) quotient of (<paramref name="dividend"/> % <paramref name="divisor"/>) and outputs the <paramref name="remainder"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>Returns the integer (truncated or floor) quotient and outputs the <paramref name="remainder"/>.</returns>
    public static TNumber TruncRem<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }

    /// <summary>
    /// <para>Computes the integer (i.e. truncated/floor) quotient of (<paramref name="dividend"/> % <paramref name="divisor"/>) and outputs the <paramref name="remainder"/> and <paramref name="remainderSign"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <param name="remainderSign"></param>
    /// <returns>Returns the integer (truncated or floor) quotient and outputs the <paramref name="remainder"/> and <paramref name="remainderSign"/>.</returns>
    public static TNumber TruncRemSgn<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder, out int remainderSign)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainderSign = TNumber.Sign(remainder = dividend % divisor);

      return (dividend - remainder) / divisor;
    }
  }
}
