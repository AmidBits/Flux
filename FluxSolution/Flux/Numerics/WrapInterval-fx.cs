//namespace Flux
//{
//  public static partial class Number
//  {
//    /// <summary>
//    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
//    /// </summary>
//    /// <typeparam name="TNumber"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="minValue"></param>
//    /// <param name="maxValue"></param>
//    /// <returns></returns>
//    //public static TNumber WrapIntervalClosed<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //  => (value > maxValue)
//    //  ? minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One))
//    //  : (value < minValue)
//    //  ? maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One))
//    //  : value;

//    //public static TNumber WrapIntervalHalfOpenLeft<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //  => (value > maxValue)
//    //  ? minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One))
//    //  : (value <= minValue)
//    //  ? maxValue - ((minValue - value) % (maxValue - minValue))
//    //  : value;

//    //public static TNumber WrapIntervalHalfOpenRight<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //  => (value >= maxValue)
//    //  ? minValue + ((value - maxValue) % (maxValue - minValue))
//    //  : (value < minValue)
//    //  ? maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One))
//    //  : value;

//    //public static TNumber WrapIntervalOpen<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //  => (value >= maxValue)
//    //  ? minValue + ((value - maxValue) % (maxValue - minValue))
//    //  : (value <= minValue)
//    //  ? maxValue - ((minValue - value) % (maxValue - minValue))
//    //  : value;

//    //public static TNumber WrapInterval<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue, IntervalNotation intervalNotation)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //{
//    //  if (value <= minValue || value >= maxValue)
//    //    return intervalNotation switch
//    //    {
//    //      IntervalNotation.Closed => WrapIntervalClosed(value, minValue, maxValue),
//    //      IntervalNotation.HalfOpenLeft when value > maxValue => WrapIntervalHalfOpenLeft(value, minValue, maxValue),
//    //      IntervalNotation.HalfOpenRight when value >= maxValue => WrapIntervalHalfOpenRight(value, minValue, maxValue),
//    //      IntervalNotation.Open when value >= maxValue => WrapIntervalOpen(value, minValue, maxValue),
//    //      _ => value,
//    //    };
//    //  else
//    //    return value;
//    //}

//    //public static TNumber Wrap<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
//    //  where TNumber : System.Numerics.INumber<TNumber>
//    //{
//    //  (minValue, maxValue) = Interval.GetExtent(minValue, maxValue, intervalNotation);

//    //  if (value > maxValue)
//    //    return minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One));

//    //  if (value < minValue)
//    //    return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One));

//    //  return value;
//    //}
//  }
//}
