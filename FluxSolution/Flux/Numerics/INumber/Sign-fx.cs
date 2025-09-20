namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>The sign step function.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Sign_function"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Step_function"/></para>
    /// </summary>
    /// <remarks>LT zero = -1, EQ zero = 0, GT zero = +1.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNumber Sign<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsZero(value) ? value : value.UnitSign();

    /// <summary>
    /// <para>The unit sign step function, i.e. zero is treated as a positive unit value of one.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Step_function"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Sign_function"/></para>
    /// </summary>
    /// <remarks>LT 0 (negative) = -1, GTE 0 (not negative) = +1.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNumber UnitSign<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(TNumber.One, value);
  }
}
