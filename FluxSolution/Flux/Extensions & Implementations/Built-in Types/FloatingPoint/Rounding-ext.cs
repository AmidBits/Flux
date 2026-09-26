namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region RoundToNearestInteger

      /// <summary>
      /// <para>Gets the nearest integer from a <typeparamref name="TFloat"/> <paramref name="value"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static TFloat RoundToNearestInteger(TFloat value, NearestRoundingRule rule = NearestRoundingRule.AwayFromZero)
      {
        if (TFloat.IsInteger(value))
          return value;

        var floor = TFloat.Floor(value);
        var fraction = value - floor;
        var half = TFloat.CreateChecked(0.5);

        var ceiling = floor + TFloat.One;

        if (fraction == half)
        {
          return rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => floor,
            NearestRoundingRule.TowardPositiveInfinity => ceiling,
            NearestRoundingRule.TowardZero => TFloat.Truncate(value),
            NearestRoundingRule.AwayFromZero => TFloat.IsNegative(value) ? floor : ceiling,
            NearestRoundingRule.ToEven => TFloat.IsEvenInteger(floor) ? floor : ceiling,
            NearestRoundingRule.ToOdd => TFloat.IsOddInteger(floor) ? floor : ceiling,
            NearestRoundingRule.Random => System.Random.Shared.Next(2) == 0 ? floor : ceiling,
            _ => throw new ArgumentOutOfRangeException(nameof(rule)),
          };
        }

        return fraction < half ? floor : ceiling; // Non-midpoint nearest-integer rounding.
      }

      #endregion
    }
  }
}
