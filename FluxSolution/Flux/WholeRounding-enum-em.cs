namespace Flux
{
  public static partial class GenericMath
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
}
