namespace Flux
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public enum IntervalNotation
  {
    /// <summary>A closed interval is an interval that includes all its endpoints and is denoted with square brackets: [left, right]</summary>
    /// <remarks>This is the default interval notation.</remarks>
    Closed,
    /// <summary>A half-open interval has two endpoints and includes only one of them. A left-open interval excludes the left endpoint: (left, right]</summary>
    HalfOpenLeft,
    /// <summary>A half-open interval has two endpoints and includes only one of them. A right-open interval excludes the right endpoint: [left, right)</summary>
    HalfOpenRight,
    /// <summary>An open interval does not include any endpoint, and is indicated with parentheses: (left, right)</summary>
    Open,
  }

  public static partial class IntervalNotationExtensions
  {
    extension(IntervalNotation source)
    {
      /// <summary>
      /// <para>Asserts that a <paramref name="value"/> is a member of an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/>. Throws an exception if it's not a member.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="intervalNotation">The interval notation to apply when asserting the value.</param>
      /// <param name="value">The value to assert.</param>
      /// <param name="minValue">The lower bound of the interval set.</param>
      /// <param name="maxValue">The upper bound of the interval set.</param>
      /// <param name="paramName">Optional parameter name for the exception.</param>
      /// <returns>The <paramref name="value"/>, if a member, otherwise an exception is thrown.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public TComparable AssertMember<TComparable>(TComparable value, TComparable minValue, TComparable maxValue, string? paramName = "value")
        where TComparable : System.IComparable<TComparable>
        => IsMember(source, value, minValue, maxValue)
        ? value
        : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Nonmember value: {{{value}}} not in {ToIntervalNotationString(source, minValue, maxValue)}.");

      /// <summary>
      /// <para>Asserts that an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/> is valid. Throws an exception if it's invalid.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="intervalNotation"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <exception cref="System.ArgumentException"></exception>
      public (TComparable minValue, TComparable maxValue) AssertValid<TComparable>(TComparable minValue, TComparable maxValue, string? paramName = "minValue/maxValue")
        where TComparable : System.IComparable<TComparable>
        => IsValid(source, minValue, maxValue)
        ? (minValue, maxValue)
        : throw new System.ArgumentException($"Invalid interval: {ToIntervalNotationString(source, minValue, maxValue)}.", paramName);

      /// <summary>
      /// <para>Compares a <paramref name="value"/> to an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="value">The value to compare.</param>
      /// <param name="minValue">The lower bound of the interval set.</param>
      /// <param name="maxValue">The upper bound of the interval set.</param>
      /// <returns>
      /// <para>The '(-or-equal)' below depends on the openess of the <see cref="IntervalNotation"/> specified in <paramref name="intervalNotation"/>.</para>
      /// <para>0 means that <paramref name="value"/> is a member of the interval.</para>
      /// <para>-1 means that <paramref name="value"/> is less-than(-or-equal) to <paramref name="minValue"/>, and hence not a member of the interval.</para>
      /// <para>+1 means that <paramref name="value"/> is greater-than(-or-equal) to <paramref name="maxValue"/>, and hence not a member of the interval.</para>
      /// </returns>
      /// <exception cref="NotImplementedException"></exception>
      public int CompareWith<TComparable>(TComparable value, TComparable minValue, TComparable maxValue)
        where TComparable : System.IComparable<TComparable>
      {
        var vcmpmin = value.CompareTo(minValue);
        var vcmpmax = value.CompareTo(maxValue);

        if (source == IntervalNotation.Closed) return vcmpmin < 0 ? -1 : vcmpmax > 0 ? +1 : 0;
        else if (source == IntervalNotation.HalfOpenLeft) return vcmpmin <= 0 ? -1 : vcmpmax > 0 ? +1 : 0;
        else if (source == IntervalNotation.HalfOpenRight) return vcmpmin < 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
        else if (source == IntervalNotation.Open) return vcmpmin <= 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
        else throw new NotImplementedException();
      }

      //public int CompareWithMinValue<TComparable>(TComparable value, TComparable minValue)
      //  where TComparable : System.IComparable<TComparable>
      //{
      //  var vcmpmin = value.CompareTo(minValue);

      //  return source switch
      //  {
      //    IntervalNotation.Closed => vcmpmin,
      //    IntervalNotation.HalfOpenLeft => vcmpmin <= 0 ? -1 : +1,
      //    IntervalNotation.HalfOpenRight => vcmpmin < 0 ? -1 : +1,
      //    IntervalNotation.Open => vcmpmin <= 0 ? -1 : +1,
      //    _ => throw new NotImplementedException()
      //  };
      //}

      //public int CompareWithMaxValue<TComparable>(TComparable value, TComparable maxValue)
      //  where TComparable : System.IComparable<TComparable>
      //{
      //  var vcmpmax = value.CompareTo(maxValue);

      //  return source switch
      //  {
      //    IntervalNotation.Closed => vcmpmax,
      //    IntervalNotation.HalfOpenLeft => vcmpmax > 0 ? +1 : -1,
      //    IntervalNotation.HalfOpenRight => vcmpmax >= 0 ? +1 : -1,
      //    IntervalNotation.Open => vcmpmax >= 0 ? +1 : -1,
      //    _ => throw new NotImplementedException()
      //  };
      //}

      /// <summary>
      /// <para>Folds an out-of-bound <paramref name="value"/> (back and forth) across the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TNumber FoldAcross<TNumber>(TNumber value, TNumber minValue, TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        (minValue, maxValue) = source.GetExtentRelative(minValue, maxValue, 1);

        return Number.FoldAcross(value, minValue, maxValue);
      }

      /// <summary>
      /// <para>Gets a new min/max interval relative type (<typeparamref name="TNumber"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
      /// <para>If <typeparamref name="TNumber"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1) and (<paramref name="maxValue"/> - 1).</para>
      /// <para>If <typeparamref name="TNumber"/> is a floating point value, magnitude > 1 and <see cref="IntervalNotation"/> is open on either or both ends, the new values are (<paramref name="minValue"/> + "epsilon" * <paramref name="magnitude"/>) and (<paramref name="maxValue"/> - "epsilon" * <paramref name="magnitude"/>). In this context epsilon is a value that makes the original value and the new value not equal.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      /// <exception cref="NotImplementedException"></exception>
      public (TNumber Minimum, TNumber Maximum) GetExtentRelative<TNumber>(TNumber minValue, TNumber maxValue, int magnitude)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        if (source != IntervalNotation.Closed)
        {
          System.ArgumentOutOfRangeException.ThrowIfNegative(magnitude);

          while (--magnitude >= 0)
          {
            if (source is IntervalNotation.Open or IntervalNotation.HalfOpenLeft)
              minValue = Number.NativeIncrement(minValue);

            if (source is IntervalNotation.Open or IntervalNotation.HalfOpenRight)
              maxValue = Number.NativeDecrement(maxValue);
          }

          AssertValid(IntervalNotation.Closed, minValue, maxValue, "relative-magnitude");
        }

        return (minValue, maxValue);
      }

      /// <summary>
      /// <para>Gets a new min/max interval by <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied "inwards" to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="minMargin"></param>
      /// <param name="maxMargin"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      /// <exception cref="NotImplementedException"></exception>
      public (TNumber minValue, TNumber maxValue) GetExtentAbsolute<TNumber>(TNumber minValue, TNumber maxValue, TNumber margin)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        if (source != IntervalNotation.Closed)
        {
          System.ArgumentOutOfRangeException.ThrowIfNegative(margin);

          if (source is IntervalNotation.Open or IntervalNotation.HalfOpenLeft)
            minValue += margin;

          if (source is IntervalNotation.Open or IntervalNotation.HalfOpenRight)
            maxValue -= margin;

          AssertValid(IntervalNotation.Closed, minValue, maxValue, "absolute-margin");
        }

        return (minValue, maxValue);
      }

      /// <summary>
      /// <para>Calculates the offset and length of an interval (<paramref name="minValue"/>, <paramref name="maxValue"/>) in a specified <see cref="IntervalNotation"/>.</para>
      /// <example>
      /// <para>Example:</para>
      /// <code>(2, 3).<see cref="IntervalNotation.Closed"/> = (2, 3)</code>
      /// <code>(2, 3).<see cref="IntervalNotation.HalfOpenLeft"/> = (3, 2)</code>
      /// <code>(2, 3).<see cref="IntervalNotation.HalfOpenRight"/> = (2, 2)</code>
      /// <code>(2, 3).<see cref="IntervalNotation.Open"/> = (3, 1)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public (int Offset, int Length) GetOffsetAndLength<TInteger>(TInteger minValue, TInteger maxValue)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        var index = int.CreateChecked(minValue);
        var length = int.CreateChecked(maxValue - minValue);

        if (source is IntervalNotation.HalfOpenLeft or IntervalNotation.Open)
          index++;

        if (source is IntervalNotation.Closed)
          length++;
        else if (source is IntervalNotation.Open)
          length--;

        if (length <= 0)
          throw new System.InvalidOperationException();

        return (index, length);
      }

      /// <summary>
      /// <para>Inverts an <see cref="IntervalNotation"/>.</para>
      /// </summary>
      /// <returns>
      /// <para>The 'opposite' <see cref="IntervalNotation"/>, i.e.:</para>
      /// <list type="bullet">
      /// <item><see cref="IntervalNotation.Closed"/> => <see cref="IntervalNotation.Open"/> </item>
      /// <item><see cref="IntervalNotation.HalfOpenLeft"/> => <see cref="IntervalNotation.HalfOpenRight"/> </item>
      /// <item><see cref="IntervalNotation.HalfOpenRight"/> => <see cref="IntervalNotation.HalfOpenLeft"/> </item>
      /// <item><see cref="IntervalNotation.Open"/> => <see cref="IntervalNotation.Closed"/> </item>
      /// </list>
      /// </returns>
      /// <exception cref="NotImplementedException"></exception>
      public IntervalNotation InvertNotation()
        => (source == IntervalNotation.Closed) ? IntervalNotation.Open
        : (source == IntervalNotation.Open) ? IntervalNotation.Closed
        : (source == IntervalNotation.HalfOpenLeft) ? IntervalNotation.HalfOpenRight
        : (source == IntervalNotation.HalfOpenRight) ? IntervalNotation.HalfOpenLeft
        : throw new NotImplementedException(source.ToString());

      /// <summary>
      /// <para>Determines whether an interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a degenerate interval, i.e. the interval consists of only a single value.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <remarks>If <paramref name="intervalNotation"/> is <see cref="IntervalNotation.Closed"/> and <paramref name="minValue"/> equals <paramref name="maxValue"/>, then it is a degenerate interval.</remarks>
      public bool IsDegenerateInterval<TComparable>(TComparable minValue, TComparable maxValue)
        where TComparable : System.IComparable<TComparable>
        => source == IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0;

      /// <summary>
      /// <para>Determines whether an interval <paramref name="minValue"/>..<paramref name="maxValue"/> with a specified <see cref="IntervalNotation"/> represents the empty set.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <remarks>
      /// <list type="bullet">
      /// <item>If <paramref name="minValue"/> equals <paramref name="maxValue"/>, all but the closed notation represents the empty set.</item>
      /// <item>If <paramref name="minValue"/> is greater than <paramref name="maxValue"/>, all four notations are usually taken to represent the empty set.</item>
      /// </list>
      /// </remarks>
      public bool IsEmptySet<TComparable>(TComparable minValue, TComparable maxValue)
        where TComparable : System.IComparable<TComparable>
        => (source != IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0) // If minValue equals maxValue, all but the closed notation represents the empty set.
        || (minValue.CompareTo(maxValue) > 0 || maxValue.CompareTo(minValue) < 0); // If minValue is greater than maxValue, all four notations are usually taken to represent the empty set.

      /// <summary>
      /// <para>Determines whether a <paramref name="value"/> is a member of an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="value">The value to verify.</param>
      /// <param name="minValue">The lower bound of the interval set.</param>
      /// <param name="maxValue">The upper bound of the interval set.</param>
      /// <returns>Whether the <paramref name="value"/> is a member of the interval.</returns>
      public bool IsMember<TComparable>(TComparable value, TComparable minValue, TComparable maxValue)
        where TComparable : System.IComparable<TComparable>
      {
        try { return CompareWith(source, value, minValue, maxValue) == 0; }
        catch { return false; }
      }

      /// <summary>
      /// <para>Determines whether an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a valid interval.</para>
      /// </summary>
      /// <typeparam name="TComparable"></typeparam>
      /// <param name="source"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public bool IsValid<TComparable>(TComparable minValue, TComparable maxValue)
        where TComparable : System.IComparable<TComparable>
        => (source == IntervalNotation.Closed && minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) >= 0)
        || (source == IntervalNotation.HalfOpenRight && minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) > 0)
        || (source == IntervalNotation.HalfOpenLeft && minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) >= 0)
        || (source == IntervalNotation.Open && minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) > 0);

      public System.Collections.Generic.IEnumerable<TNumber> Loop<TNumber>(TNumber minValue, TNumber maxValue, TNumber stepSize, IntervalNotation intervalNotation = IntervalNotation.Closed)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);

        (minValue, maxValue) = intervalNotation.GetExtentAbsolute(minValue, maxValue, TNumber.Abs(stepSize));

        return TNumber.IsNegative(stepSize)
          ? Number.ArithmeticSequence(maxValue, stepSize).TakeWhile(n => n >= minValue)
          : Number.ArithmeticSequence(minValue, stepSize).TakeWhile(n => n <= maxValue);
      }

      /// <summary>
      /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) on the left hand side.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public char ToIntervalNotationCharLeft()
        => (source is IntervalNotation.Closed or IntervalNotation.HalfOpenRight) ? '['
        : (source is IntervalNotation.Open or IntervalNotation.HalfOpenLeft) ? '('
        : throw new NotImplementedException();

      /// <summary>
      /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) on the right hand side.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public char ToIntervalNotationCharRight()
        => (source is IntervalNotation.Closed or IntervalNotation.HalfOpenLeft) ? ']'
        : (source is IntervalNotation.Open or IntervalNotation.HalfOpenRight) ? ')'
        : throw new NotImplementedException();

      /// <summary>
      /// <para>Creates a string representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) of the interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public string ToIntervalNotationString<T>(T minValue, T maxValue, string? format = null, System.IFormatProvider? provider = null)
      {
        format = $"{{0{(format is null ? string.Empty : $":{format}")}}}";

        return $"{source.ToIntervalNotationCharLeft()}{string.Format(provider, format, minValue)}, {string.Format(provider, format, maxValue)}{source.ToIntervalNotationCharRight()}";
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <para>The Start property of Range is inclusive, but the End property is exclusive.</para>
      /// <para>Both the Min and the Max properties on an Interval are inclusive.</para>
      /// </remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Range ToRange<TInteger>(TInteger minValue, TInteger maxValue)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        (minValue, maxValue) = source.GetExtentAbsolute(minValue, maxValue, TInteger.One);

        return new(int.CreateChecked(minValue), int.CreateChecked(maxValue) + 1);
      }

      /// <summary>
      /// <para>Wraps around using an absolute margin on 1 for all types.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TNumber WrapAround<TNumber>(TNumber value, TNumber minValue, TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        (minValue, maxValue) = source.GetExtentAbsolute(minValue, maxValue, TNumber.One);

        return Number.WrapAround(value, minValue, maxValue);
      }

      /// <summary>
      /// <para>Wraps around using the native values of open ends (1 for integer types and bit increment/decrement for floating point).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TNumber WrapAroundNative<TNumber>(TNumber value, TNumber minValue, TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        (minValue, maxValue) = source.GetExtentRelative(minValue, maxValue, 1);

        return Number.WrapAround(value, minValue, maxValue);
      }

      //public TNumber Wrap<TNumber>(TNumber value, TNumber minValue, TNumber maxValue)
      //  where TNumber : System.Numerics.INumber<TNumber>
      //{
      //  (minValue, maxValue) = source.GetExtentAbsolute(minValue, maxValue, 0);

      //  return NumberFunctions.WrapAround(value, minValue, maxValue);
      //}
    }
  }
}
