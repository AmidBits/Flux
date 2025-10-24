namespace Flux
{
  /// <summary>
  /// <para>The strategy of rounding to a number, when a number is anywhere between two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum IntegralRounding
  {
    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// </summary>
    ToEven = 100 + MidpointRounding.ToEven,

    /// <summary>
    /// <para>Round to the number away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This mode is the opposite of truncating.</para>
    /// <para>Symmetric rounding: round up, bias: away from zero.</para>
    /// </remarks>
    AwayFromZero = 100 + MidpointRounding.AwayFromZero,

    /// <summary>
    /// <para>Round to the number towards-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Truncate of the number.</para>
    /// <para>Symmetric rounding: round down, bias: towards zero.</para>
    /// </remarks>
    TowardZero = 100 + MidpointRounding.ToZero,

    /// <summary>
    /// <para>Round down to the number less-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Floor of the number.</para>
    /// <para>Common rounding: round down, bias: negative infinity.</para>
    /// </remarks>
    ToNegativeInfinity = 100 + MidpointRounding.ToNegativeInfinity,

    /// <summary>
    /// <para>Round up to the number greater-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Ceiling of the number.</para>
    /// <para>Common rounding: round up, bias: positive infinity.</para>
    /// </remarks>
    ToPositiveInfinity = 100 + MidpointRounding.ToPositiveInfinity,

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// </summary>
    ToOdd = 100 + HalfRounding.ToOdd,

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>Round to down or up randomly.</para>
    /// </summary>
    /// <remarks>
    /// <para>Random rounding: down or up, bias: none (though the RNG might be).</para>
    /// </remarks>
    ToRandom = 100 + HalfRounding.ToRandom,
  }

  public static class XtensionIntegralRounding
  {
    public static TFloat RoundToIntegral<TFloat>(this TFloat value, IntegralRounding mode)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => mode switch
      {
        IntegralRounding.ToEven => RoundToEvenIntegral(value),
        IntegralRounding.AwayFromZero => value.Envelop(),
        IntegralRounding.TowardZero => TFloat.Truncate(value),
        IntegralRounding.ToNegativeInfinity => TFloat.Floor(value),
        IntegralRounding.ToPositiveInfinity => TFloat.Ceiling(value),
        IntegralRounding.ToRandom => RoundToRandomIntegral(value),
        IntegralRounding.ToOdd => RoundToOddIntegral(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>
    /// <para>Try to round <paramref name="value"/> using whole-rounding (unconditional) rounding <paramref name="mode"/> into the out parameter <paramref name="result"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryRoundToIntegral<TFloat>(this TFloat value, IntegralRounding mode, out TFloat result)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      try
      {
        result = value.RoundToIntegral(mode);
        return true;
      }
      catch
      {
        result = default!;
        return false;
      }
    }

    /// <summary>
    /// <para><see cref="IntegralRounding.ToEven"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundToEvenIntegral<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => TFloat.Floor(value) is var floor && TFloat.IsEvenInteger(floor)
      ? floor
      : TFloat.Ceiling(value);

    /// <summary>
    /// <para><see cref="IntegralRounding.ToOdd"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundToOddIntegral<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => TFloat.Floor(value) is var floor && TFloat.IsOddInteger(floor)
      ? floor
      : TFloat.Ceiling(value);

    /// <summary>
    /// <para><see cref="IntegralRounding.ToRandom"/></para>
    /// </summary>
    /// <returns></returns>
    public static TFloat RoundToRandomIntegral<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => RandomNumberGenerators.SscRng.Shared.Next(2) == 0 ? TFloat.Floor(value) : TFloat.Ceiling(value);
  }
}
