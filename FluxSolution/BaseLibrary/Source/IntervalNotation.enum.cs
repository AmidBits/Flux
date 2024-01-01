namespace Flux
{
  public static partial class Em
  {
    /// <summary>Asserts that the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>, and throws an exception if it's not.</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public static TSource AssertMember<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue, string? paramName = null)
      where TSource : System.IComparable<TSource>
      => source switch
      {
        IntervalNotation.Closed => source.IsMember(value, minValue, maxValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {minValue} and less-than-or-equal-to {maxValue}."),
        IntervalNotation.Open => source.IsMember(value, minValue, maxValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {minValue} and less-than {maxValue}."),
        IntervalNotation.HalfOpenLeft => source.IsMember(value, minValue, maxValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {minValue} and less-than-or-equal-to {maxValue}."),
        IntervalNotation.HalfOpenRight => source.IsMember(value, minValue, maxValue) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {minValue} and less-than {maxValue}."),
        _ => throw new NotImplementedException(),
      };

    /// <summary>Returns the actual minimum and maximum integer of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    public static (TSource ActualMinimum, TSource ActualMaximum) GetExtremumBI<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
      where TSource : System.Numerics.IBinaryInteger<TSource>
      => source switch
      {
        IntervalNotation.Closed => (minValue, maxValue),
        IntervalNotation.Open => (minValue + TSource.One, maxValue - TSource.One),
        IntervalNotation.HalfOpenLeft => (minValue + TSource.One, maxValue),
        IntervalNotation.HalfOpenRight => (minValue, maxValue - TSource.One),
        _ => throw new NotImplementedException(),
      };

    /// <summary>Returns the actual minimum and maximum integer of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    public static (TSource ActualMinimum, TSource ActualMaximum) GetExtremumFP<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
      where TSource : System.Numerics.IFloatingPointIeee754<TSource>
      => source switch
      {
        IntervalNotation.Closed => (minValue, maxValue),
        IntervalNotation.Open => (TSource.BitIncrement(minValue), TSource.BitDecrement(maxValue)),
        IntervalNotation.HalfOpenLeft => (TSource.BitIncrement(minValue), maxValue),
        IntervalNotation.HalfOpenRight => (minValue, TSource.BitDecrement(maxValue)),
        _ => throw new NotImplementedException(),
      };

    /// <summary>Determines whether the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public static bool IsMember<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue)
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
    /// <summary>A closed interval is an interval which includes all its limit points, and is denoted with square brackets: "[ low , high ]"</summary>
    Closed,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the lower, or left side, value: "( low , high ]"</remarks>
    HalfOpenLeft,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the higher, or right side, value: "[ low , high )"</remarks>
    HalfOpenRight,
    /// <summary>An open interval does not include its endpoints, and is indicated with parentheses: "( low , high )"</summary>
    Open,
  }
}
