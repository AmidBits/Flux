namespace Flux
{
  public static partial class GenericMath
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

    #region RoundHalfAlternating

    private static bool m_roundHalfAlternatingState; // This is used for internal state.

    /// <summary>
    /// <para></para>
    /// <para><see cref="HalfRounding.Alternating"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundHalfAlternating<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => value.RoundHalfAlternating(ref m_roundHalfAlternatingState);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public static TFloat RoundHalfAlternating<TFloat>(this TFloat value, ref bool state)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return (state = !state) ? floor : ceiling;
    }

    #endregion // RoundHalfAlternating

    #region RoundHalfRandom

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToRandom"/></para>
    /// </remarks>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TFloat RoundHalfRandom<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => RoundHalfRandom(value, Flux.Randomness.NumberGenerators.SscRng.Shared);

    /// <summary>
    /// <para></para>
    /// <para><see cref="HalfRounding.Random"/></para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundHalfRandom<TFloat>(this TFloat value, System.Random rng)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return (rng ?? Randomness.NumberGenerators.SscRng.Shared).NextDouble() < 0.5 ? floor : ceiling;
    }

    #endregion // RoundHalfRandom

    #region RoundHalfToOdd

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// <para><see cref="HalfRounding.ToOdd"/></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToOdd"/></para>
    /// </remarks>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundHalfToOdd<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return TFloat.IsOddInteger(floor) ? floor : ceiling;
    }

    #endregion // RoundHalfToOdd

    public static (TFloat Floor, TFloat Ceiling) GetFloorAndCeilingWithForgiveness<TFloat>(this TFloat value, double tolerance = 1e-10)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var floor = TFloat.Floor(value);
      var ceiling = TFloat.Ceiling(value);

      if (floor != ceiling)
      {
        var rounded = TFloat.CreateChecked(TFloat.Round(value));
        var epsilon = TFloat.CreateChecked(tolerance);

        if (value.EqualsWithinTolerance(rounded, epsilon, epsilon))
        {
          if (value < rounded)
            floor = ceiling;

          if (value > rounded)
            ceiling = floor;
        }
      }

      return (floor, ceiling);
    }

    //public static TFloat RoundWithTolerance<TFloat>(this TFloat value, HalfRounding mode, double tolerance = 1e-10)
    //  where TFloat : System.Numerics.IFloatingPoint<TFloat>
    //{
    //  var rounded = value.RoundHalf(mode);
    //  var epsilon = TFloat.CreateChecked(tolerance);

    //  if (value.EqualsWithinAbsoluteTolerance(rounded, epsilon) || value.EqualsWithinRelativeTolerance(rounded, epsilon))
    //    return true;

    //  return false;
    //}
  }
}
