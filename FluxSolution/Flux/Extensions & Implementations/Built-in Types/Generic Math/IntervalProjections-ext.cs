namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Spread.. functions

      /// <summary>
      /// <para>Spreads an in-interval value around the outside edges of the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="minValue"/>) or (<paramref name="maxValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="minValue"/> and <paramref name="maxValue"/>.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="rule"></param>
      /// <param name="margin"></param>
      /// <returns></returns>
      public static TNumber Spread(TNumber value, TNumber minValue, TNumber maxValue, NearestRoundingRule rule, TNumber margin)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(margin);

        if (value < minValue || value > maxValue)
          return value; // If number is already spread, nothing to do but return it.

        var nearestValue = RoundToNearestValue(value, rule, false, minValue, maxValue);

        return (nearestValue == minValue)
          ? minValue - margin
          : (nearestValue == maxValue)
          ? maxValue + margin
          : nearestValue;
      }

      /// <summary>
      /// <para>Spreads an in-interval value around the outside edges of the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="minValue"/>) or (<paramref name="maxValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="minValue"/> and <paramref name="maxValue"/>.</para>
      /// <para>A native function means that the difference will be based on the smallest native difference. For integers it is 1, and for floating point (double and float) it is the bit increment/decrement.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="rule"></param>
      /// <returns></returns>
      public static TNumber UlpSpread(TNumber value, TNumber minValue, TNumber maxValue, NearestRoundingRule rule)
      {
        if (value < minValue || value > maxValue)
          return value; // If number is already spread, nothing to do but return it.

        var nearestValue = RoundToNearestValue(value, rule, false, minValue, maxValue);

        return (nearestValue == minValue)
          ? UlpDecrement(minValue)
          : (nearestValue == maxValue)
          ? UlpIncrement(maxValue)
          : nearestValue;
      }

      #endregion
    }
  }
}
