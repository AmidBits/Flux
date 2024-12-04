namespace Flux
{
  public static partial class Em
  {
    public static TValue RoundWhole<TValue>(this TValue value, WholeRounding mode)
    where TValue : System.Numerics.IFloatingPoint<TValue>
    => mode switch
    {
      WholeRounding.ToEven => value.RoundWholeToEven(),
      WholeRounding.AwayFromZero => value.Envelop(),
      WholeRounding.TowardZero => TValue.Truncate(value),
      WholeRounding.ToNegativeInfinity => TValue.Floor(value),
      WholeRounding.ToPositiveInfinity => TValue.Ceiling(value),
      WholeRounding.Random => value.RoundToNearestRandom(TValue.Floor(value), TValue.Ceiling(value)),
      WholeRounding.Alternating => value.RoundToNearestAlternating(TValue.Floor(value), TValue.Ceiling(value)),
      WholeRounding.ToOdd => value.RoundWholeToOdd(),
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
    public static bool TryRoundWhole<TValue>(this TValue value, WholeRounding mode, out TValue result)
      where TValue : System.Numerics.IFloatingPoint<TValue>
    {
      try
      {
        result = value.RoundWhole(mode);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundWholeToEven<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value) is var floor && TValue.IsEvenInteger(floor)
      ? floor
      : TValue.Ceiling(value);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundWholeToOdd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => TValue.Floor(value) is var floor && TValue.IsOddInteger(floor)
      ? floor
      : TValue.Ceiling(value);
  }

  /// <summary>
  /// <para>The strategy of rounding to a number, when a number is anywhere between two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum WholeRounding
  {
    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// </summary>
    ToEven = 100, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>Round to the number away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This mode is the opposite of truncating.</para>
    /// <para>Symmetric rounding: round up, bias: away from zero.</para>
    /// </remarks>
    AwayFromZero = 101,

    /// <summary>
    /// <para>Round to the number towards-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Truncate of the number.</para>
    /// <para>Symmetric rounding: round down, bias: towards zero.</para>
    /// </remarks>
    TowardZero = 102,

    /// <summary>
    /// <para>Round down to the number less-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Floor of the number.</para>
    /// <para>Common rounding: round down, bias: negative infinity.</para>
    /// </remarks>
    ToNegativeInfinity = 103,

    /// <summary>
    /// <para>Round up to the number greater-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Ceiling of the number.</para>
    /// <para>Common rounding: round up, bias: positive infinity.</para>
    /// </remarks>
    ToPositiveInfinity = 104,

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>Round to down or up randomly.</para>
    /// </summary>
    /// <remarks>
    /// <para>Random rounding: down or up, bias: none (though the RNG might be).</para>
    /// </remarks>
    Random = 108, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// </summary>
    Alternating = 109, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// </summary>
    ToOdd = 110, // There is no built-in rounding of this kind.
  }
}
