namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Computes a non-zero remainder of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [divisor, 1 .. (divisor - 1)], instead of the remainder [0, 1 .. (divisor - 1)].</para>
    /// <para>Zero is replaced by the <paramref name="divisor"/> (with the sign of <paramref name="dividend"/>) and all other remainder (%) values returned as defined by <typeparamref name="TNumber"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns>
    /// <para>The reverse remainder of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    /// </returns>
    public static TNumber RemainderNonZero<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainder = dividend % divisor;

      return TNumber.IsZero(remainder) ? TNumber.CopySign(divisor, dividend) : remainder;
    }

    /// <summary>
    /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [0, (divisor - 1) .. 1] instead of the remainder [0, 1 .. (divisor - 1)].</para>
    /// <para>All remainder values greater-than zero are in reverse order from that defined by <typeparamref name="TNumber"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>
    /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    /// </returns>
    public static TNumber ReverseRemainderWithZero<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainder = dividend % divisor;

      return TNumber.IsZero(remainder) ? remainder : TNumber.CopySign(divisor, dividend) - remainder;
    }

    /// <summary>
    /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [divisor, 1], instead of the remainder [0, (divisor - 1)].</para>
    /// <para>Zero is replaced by the <paramref name="divisor"/> with the sign of <paramref name="dividend"/>, and all other remainder (%) values are returned in reverse order from that defined by <typeparamref name="TNumber"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns>
    /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    /// </returns>
    public static TNumber ReverseRemainderNonZero<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var rev = ReverseRemainderWithZero(dividend, divisor, out remainder);

      return TNumber.IsZero(rev) ? TNumber.CopySign(divisor, dividend) : rev;
    }
  }
}
