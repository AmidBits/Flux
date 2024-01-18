namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> using the specified <see cref="IntervalNotation"/>, and throws an exception if it's not.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The interval notation to apply when asserting the value.</param>
    /// <param name="value">The value to assert.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <param name="paramName">Optional parameter name for the exception.</param>
    /// <returns>The <paramref name="value"/>, if a member, otherwise an exception is thrown.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static TSource AssertMember<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue, string? paramName = null)
      where TSource : System.IComparable<TSource>
      => source switch
      {
        IntervalNotation.Closed => source.VerifyMember(value, minValue, maxValue) ? value
          : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Value ({value}) must be greater-than-or-equal-to {minValue} and less-than-or-equal-to {maxValue}."),
        IntervalNotation.Open => source.VerifyMember(value, minValue, maxValue) ? value
          : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Value ({value}) must be greater-than {minValue} and less-than {maxValue}."),
        IntervalNotation.HalfOpenLeft => source.VerifyMember(value, minValue, maxValue) ? value
          : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Value ({value}) must be greater-than {minValue} and less-than-or-equal-to {maxValue}."),
        IntervalNotation.HalfOpenRight => source.VerifyMember(value, minValue, maxValue) ? value
          : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Value ({value}) must be greater-than-or-equal-to {minValue} and less-than {maxValue}."),
        _ => throw new NotImplementedException(),
      };

    ///// <summary>Asserts that the <paramref name="value"/> is an upper member of the interval set beginning at <paramref name="minValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>, and throws an exception if it's not.</summary>
    ///// <exception cref="System.NotImplementedException"></exception>
    //public static TSource AssertMember<TSource>(this IntervalNotation source, TSource value, TSource minValue, string? paramName = null)
    //  where TSource : System.IComparable<TSource>
    //  => source switch
    //  {
    //    IntervalNotation.Closed => source.IsMember(value, minValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {minValue}."),
    //    IntervalNotation.Open => source.IsMember(value, minValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {minValue}."),
    //    IntervalNotation.HalfOpenLeft => source.IsMember(value, minValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {minValue}."),
    //    IntervalNotation.HalfOpenRight => source.IsMember(value, minValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {minValue}."),
    //    _ => throw new NotImplementedException(),
    //  };

    /// <summary>
    /// <para>Compares <paramref name="value"/> with the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> using the specified <see cref="IntervalNotation"/>.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The interval notation to apply when comparing to the interval set.</param>
    /// <param name="value">The value to compare.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <returns>0 if within the interval, -1 if less than the interval, and +1 if greater than the interval.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int CompareTo<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue)
      where TSource : System.IComparable<TSource>
      => source switch
      {
        IntervalNotation.Closed => value.CompareTo(minValue) < 0 ? -1 : value.CompareTo(maxValue) > 0 ? +1 : 0,
        IntervalNotation.Open => value.CompareTo(minValue) <= 0 ? -1 : value.CompareTo(maxValue) >= 0 ? +1 : 0,
        IntervalNotation.HalfOpenLeft => value.CompareTo(minValue) <= 0 ? -1 : value.CompareTo(maxValue) > 0 ? +1 : 0,
        IntervalNotation.HalfOpenRight => value.CompareTo(minValue) < 0 ? -1 : value.CompareTo(maxValue) >= 0 ? +1 : 0,
        _ => throw new NotImplementedException(),
      };

    //public static TSource NextLargerValue<TSource>(this TSource value)
    //  where TSource : System.Numerics.INumber<TSource>
    //  => value switch
    //  {
    //    System.Double v => TSource.CreateChecked(System.Double.BitIncrement(v)),
    //    System.Single v => TSource.CreateChecked(System.Single.BitIncrement(v)),
    //    System.Half v => TSource.CreateChecked(System.Half.BitIncrement(v)),
    //    System.Runtime.InteropServices.NFloat v => TSource.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(v)),
    //    _ => value + TSource.One,
    //  };

    //public static TSource NextSmallerValue<TSource>(this TSource value)
    //  where TSource : System.Numerics.INumber<TSource>
    //  => value switch
    //  {
    //    System.Double v => TSource.CreateChecked(System.Double.BitDecrement(v)),
    //    System.Single v => TSource.CreateChecked(System.Single.BitDecrement(v)),
    //    System.Half v => TSource.CreateChecked(System.Half.BitDecrement(v)),
    //    System.Runtime.InteropServices.NFloat v => TSource.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(v)),
    //    _ => value - TSource.One,
    //  };

    //public static (TSource MinValue, TSource MaxValue) GetExtremum<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
    //  where TSource : System.Numerics.IBinaryInteger<TSource>
    //  => source switch
    //  {
    //    IntervalNotation.Closed => (minValue, maxValue),
    //    IntervalNotation.Open => (minValue.NextLargerValue(), maxValue.NextSmallerValue()),
    //    IntervalNotation.HalfOpenLeft => (minValue.NextLargerValue(), maxValue),
    //    IntervalNotation.HalfOpenRight => (minValue, maxValue.NextSmallerValue()),
    //    _ => throw new NotImplementedException(),
    //  };

    //public static TSource DecreaseValue<TSource>(this TSource value) where TSource : System.Numerics.IFloatingPointIeee754<TSource> => TSource.BitDecrement(value);
    //public static TSource IncreaseValue<TSource>(this TSource value) where TSource : System.Numerics.IFloatingPointIeee754<TSource> => TSource.BitIncrement(value);

    /// <summary>Returns the MinValue and MaxValue for the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    public static (TSource MinValue, TSource MaxValue) GetExtremumBI<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
      where TSource : System.Numerics.IBinaryInteger<TSource>
      => source switch
      {
        IntervalNotation.Closed => (minValue, maxValue),
        IntervalNotation.Open => (minValue + TSource.One, maxValue - TSource.One),
        IntervalNotation.HalfOpenLeft => (minValue + TSource.One, maxValue),
        IntervalNotation.HalfOpenRight => (minValue, maxValue - TSource.One),
        _ => throw new NotImplementedException(),
      };

    /// <summary>Returns the MinValue and MaxValue for the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    public static (TSource MinValue, TSource MaxValue) GetExtremumFP<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
      where TSource : System.Numerics.IFloatingPointIeee754<TSource>
      => source switch
      {
        IntervalNotation.Closed => (minValue, maxValue),
        IntervalNotation.Open => (TSource.BitIncrement(minValue), TSource.BitDecrement(maxValue)),
        IntervalNotation.HalfOpenLeft => (TSource.BitIncrement(minValue), maxValue),
        IntervalNotation.HalfOpenRight => (minValue, TSource.BitDecrement(maxValue)),
        _ => throw new NotImplementedException(),
      };

    ///// <summary>Determines whether the <paramref name="value"/> is an upper member of the interval set lower bound <paramref name="minValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    ///// <exception cref="System.NotImplementedException"></exception>
    //public static bool IsMember<TSource>(this IntervalNotation source, TSource value, TSource minValue)
    //  where TSource : System.IComparable<TSource>
    //  => source switch
    //  {
    //    IntervalNotation.Closed => value.CompareTo(minValue) >= 0,
    //    IntervalNotation.Open => value.CompareTo(minValue) > 0,
    //    IntervalNotation.HalfOpenLeft => value.CompareTo(minValue) >= 0,
    //    IntervalNotation.HalfOpenRight => value.CompareTo(minValue) > 0,
    //    _ => throw new NotImplementedException(),
    //  };

    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The interval notation to apply when determining membership of the interval set.</param>
    /// <param name="value">The value to verify.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <returns>Whether the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> using the <see cref="IntervalNotation"/>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static bool VerifyMember<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue)
      where TSource : System.IComparable<TSource>
      => source switch
      {
        IntervalNotation.Closed => value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0,
        IntervalNotation.Open => value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0,
        IntervalNotation.HalfOpenLeft => value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) < 0,
        IntervalNotation.HalfOpenRight => value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) <= 0,
        _ => throw new NotImplementedException(),
      };
  }

  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public enum IntervalNotation
  {
    /// <summary>A closed interval is an interval which includes all its limit points, and is denoted with square brackets: [min , max]</summary>
    Closed,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the minimum value: (min, max]</remarks>
    HalfOpenLeft,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the maximum value: [min, max)</remarks>
    HalfOpenRight,
    /// <summary>An open interval does not include its endpoints, and is indicated with parentheses: (min , max)</summary>
    Open,
  }
}
