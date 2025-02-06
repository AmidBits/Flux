namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified rounding <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue RoundHalf<TValue>(this TValue value, HalfRounding mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => mode switch
      {
        HalfRounding.ToEven or
        HalfRounding.AwayFromZero or
        HalfRounding.TowardZero or
        HalfRounding.ToNegativeInfinity or
        HalfRounding.ToPositiveInfinity => TValue.Round(value, (MidpointRounding)(int)mode),
        HalfRounding.Random => value.RoundHalfRandom(),
        HalfRounding.Alternating => value.RoundHalfAlternating(),
        HalfRounding.ToOdd => value.RoundHalfToOdd(),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>
    /// <para>Try to round <paramref name="value"/> using half-rounding <paramref name="mode"/> into the out parameter <paramref name="result"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryRoundHalf<TValue>(this TValue value, HalfRounding mode, out TValue result)
      where TValue : System.Numerics.IFloatingPoint<TValue>
    {
      try
      {
        result = value.RoundHalf(mode);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToRandom"/></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TValue RoundHalfRandom<TValue>(this TValue value, System.Random? rng = null)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var comparison
      && (TValue.Floor(value) is var floor && comparison <= -1)
      ? floor
      : (TValue.Ceiling(value) is var ceiling && comparison >= 1)
      ? ceiling
      : value.RoundToNearestRandom(floor, ceiling, rng);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundHalfAlternating<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var cmp
      && (TValue.Floor(value) is var floor && cmp <= -1)
      ? floor
      : (TValue.Ceiling(value) is var ceiling && cmp >= 1)
      ? ceiling
      : value.RoundToNearestAlternating(floor, ceiling);

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToOdd"/></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundHalfToOdd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var cmp
      && (cmp <= -1)
      ? TValue.Floor(value)
      : (cmp >= 1)
      ? TValue.Ceiling(value)
      : value.RoundWholeToOdd();
  }
}
