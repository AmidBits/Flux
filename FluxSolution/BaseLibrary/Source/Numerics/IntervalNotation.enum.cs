namespace Flux
{
  #region ExtensionMethods

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
    public static int CompareWith<TSource>(this IntervalNotation source, TSource value, TSource minValue, TSource maxValue)
      where TSource : System.IComparable<TSource>
      => maxValue.CompareTo(minValue) < 0
      ? throw new System.ArgumentOutOfRangeException($"Invalid interval: {source.ToNotationString(minValue, maxValue)}")
      : source switch
      {
        IntervalNotation.Closed => value.CompareTo(minValue) < 0 ? -1 : value.CompareTo(maxValue) > 0 ? +1 : 0,
        IntervalNotation.Open => value.CompareTo(minValue) <= 0 ? -1 : value.CompareTo(maxValue) >= 0 ? +1 : 0,
        IntervalNotation.HalfOpenLeft => value.CompareTo(minValue) <= 0 ? -1 : value.CompareTo(maxValue) > 0 ? +1 : 0,
        IntervalNotation.HalfOpenRight => value.CompareTo(minValue) < 0 ? -1 : value.CompareTo(maxValue) >= 0 ? +1 : 0,
        _ => throw new NotImplementedException(),
      };

    /// <summary>
    /// <para>Gets new appropriate (depending on type <typeparamref name="T"/>) values as min-value and max-value using the specified <see cref="IntervalNotation"/>.</para>
    /// <para>If <typeparamref name="T"/> is an integer, the new values are <paramref name="minValue"/> + 1 and <paramref name="maxValue"/> - 1.</para>
    /// <para>If <typeparamref name="T"/> is a floating point value, the new values are <paramref name="minValue"/> + epsilon and <paramref name="maxValue"/> - epsilon. In this context epsilon is a value that makes the original value and the new value not equal.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static (T MinValue, T MaxValue) GetInfimumAndSupremum<T>(this IntervalNotation source, T minValue, T maxValue)
      where T : System.Numerics.INumber<T>
      => maxValue.CompareTo(minValue) < 0
      ? throw new System.ArgumentOutOfRangeException($"Invalid interval: {source.ToNotationString(minValue, maxValue)}")
      : source switch
      {
        IntervalNotation.Closed => (minValue, maxValue),
        IntervalNotation.Open => (minValue.GetSupremum(), maxValue.GetInfimum()),
        IntervalNotation.HalfOpenLeft => (minValue.GetSupremum(), maxValue),
        IntervalNotation.HalfOpenRight => (minValue, maxValue.GetInfimum()),
        _ => throw new NotImplementedException(),
      };

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
      => maxValue.CompareTo(minValue) < 0
      ? throw new System.ArgumentOutOfRangeException($"Invalid interval: {source.ToNotationString(minValue, maxValue)}")
      : source switch
      {
        IntervalNotation.Closed => value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0,
        IntervalNotation.Open => value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0,
        IntervalNotation.HalfOpenLeft => value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) < 0,
        IntervalNotation.HalfOpenRight => value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) <= 0,
        _ => throw new NotImplementedException(),
      };

    public static string ToNotationString<TSource>(this IntervalNotation source, TSource minValue, TSource maxValue)
      => source switch
      {
        IntervalNotation.Closed => $"[{minValue}, {maxValue}]",
        IntervalNotation.Open => $"({minValue}, {maxValue})",
        IntervalNotation.HalfOpenLeft => $"({minValue}, {maxValue}]",
        IntervalNotation.HalfOpenRight => $"[{minValue}, {maxValue})",
        _ => throw new NotImplementedException(),
      };
  }

  #endregion // ExtensionMethods

  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public enum IntervalNotation
  {
    /// <summary>A closed interval is an interval which includes all its limit points, and is denoted with square brackets: [min , max]</summary>
    Closed,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the minimum value, whereas the maximum value is closed, i.e included: (min, max]</remarks>
    HalfOpenLeft,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the maximum value, whereas the minimum value is closed, i.e included: [min, max)</remarks>
    HalfOpenRight,
    /// <summary>An open interval does not include its endpoints, and is indicated with parentheses: (min , max)</summary>
    Open,
  }
}
