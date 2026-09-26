namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsMultipleOf

      /// <summary>
      /// Determines whether <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>.
      /// </summary>
      /// <remarks>Uses Euclidean modulo so the sign of <paramref name="value"/> does not affect the result.
      /// Throws <see cref="System.ArgumentOutOfRangeException"/> if <paramref name="multiple"/> is less than or equal
      /// to zero.</remarks>
      /// <param name="value">The value to test for being a multiple.</param>
      /// <param name="multiple">The positive divisor to test against. Must be greater than zero.</param>
      /// <returns>True if <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>; otherwise, false.</returns>
      public static bool IsMultipleOf(TNumber value, TNumber multiple)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);

        return EuclideanModulo(value, multiple) == TNumber.Zero;
      }

      #endregion

      #region RoundToMultiple

      public static TNumber RoundToMultiple(TNumber value, TNumber multiple, DirectedRoundingMode mode)
      {
        var quotient = mode switch
        {
          DirectedRoundingMode.TowardNegativeInfinity => IntegerDivRemFloored(value, multiple).Quotient,
          DirectedRoundingMode.TowardPositiveInfinity => IntegerDivRemCeiling(value, multiple).Quotient,
          DirectedRoundingMode.TowardZero => IntegerDivRemTruncated(value, multiple).Quotient,
          DirectedRoundingMode.AwayFromZero => IntegerDivRemEnveloped(value, multiple).Quotient,
          _ => throw new System.NotImplementedException(),
        };

        return quotient * multiple;
      }

      #endregion

      #region RoundToNearest

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="rule"></param>
      /// <param name="proper"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      /// <exception cref="System.NullReferenceException"></exception>
      /// <exception cref="NotImplementedException"></exception>
      public static TNumber RoundToNearest(TNumber value, NearestRoundingRule rule, bool proper, params System.ReadOnlySpan<TNumber> values)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(values.Length);

        var closestValues = new System.Collections.Generic.List<TNumber>() { values[0] };
        var closestDistance = TNumber.Abs(value - TNumber.CreateChecked(values[0]));

        for (var i = 1; i < values.Length; i++)
        {
          var currentValue = values[i];
          var currentDistance = TNumber.Abs(value - TNumber.CreateChecked(currentValue));

          if ((!proper || currentValue != value) && currentDistance <= closestDistance)
          {
            if (currentDistance < closestDistance)
            {
              closestValues.Clear();
              closestDistance = currentDistance;
            }

            if (!closestValues.Contains(currentValue))
              closestValues.Add(currentValue);
          }
        }

        return rule switch // If the distances are equal, i.e. the value is exactly halfway to all closestValues, we use the appropriate rounding strategy to resolve a winner.
        {
          NearestRoundingRule.TowardNegativeInfinity => closestValues.Min() ?? throw new System.NullReferenceException(),
          NearestRoundingRule.TowardPositiveInfinity => closestValues.Max() ?? throw new System.NullReferenceException(),
          NearestRoundingRule.TowardZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (infimumIndex > -1 ? infimumValue : supremumValue) : (supremumIndex > -1 ? supremumValue : infimumValue),
          NearestRoundingRule.AwayFromZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (supremumIndex > -1 ? supremumValue : infimumValue) : (infimumIndex > -1 ? infimumValue : supremumValue),
          NearestRoundingRule.ToEven => closestValues.FirstOrValue(closestValues[0], TNumber.IsEvenInteger).Item,
          NearestRoundingRule.ToOdd => closestValues.FirstOrValue(closestValues[0], TNumber.IsOddInteger).Item,
          NearestRoundingRule.Random => closestValues.AsSpan().GetRandomElement(),
          _ => throw new NotImplementedException(),
        };
      }

      #endregion
    }
  }
}
