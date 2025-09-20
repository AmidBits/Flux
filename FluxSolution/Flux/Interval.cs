namespace Flux
{
  public static class Interval
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the interval <paramref name="minValue"/>..<paramref name="maxValue"/> according to the specified <see cref="IntervalNotation"/>, and throws an exception if it's not.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="intervalNotation">The interval notation to apply when asserting the value.</param>
    /// <param name="value">The value to assert.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <param name="paramName">Optional parameter name for the exception.</param>
    /// <returns>The <paramref name="value"/>, if a member, otherwise an exception is thrown.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static T AssertMember<T>(T value, T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed, string? paramName = "value")
      where T : System.IComparable<T>
      => IsMember(value, minValue, maxValue, intervalNotation)
      ? value
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"The value '{value}' is not a member of the interval: {ToIntervalNotationString(minValue, maxValue, intervalNotation)}.");

    /// <summary>
    /// <para>Asserts whether the interval is valid, i.e. throws an exception if it is not.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="intervalNotation"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <exception cref="System.ArgumentException"></exception>
    public static (T minValue, T maxValue) AssertValid<T>(T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed, string? paramName = "minValue/maxValue")
      where T : System.IComparable<T>
      => IsValid(intervalNotation, minValue, maxValue)
      ? (minValue, maxValue)
      : throw new System.ArgumentException($"Invalid interval: {ToIntervalNotationString(minValue, maxValue, intervalNotation)}.", paramName);

    /// <summary>
    /// <para>Compares <paramref name="value"/> to the interval <paramref name="minValue"/>..<paramref name="maxValue"/> using the specified <see cref="IntervalNotation"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="intervalNotation">The interval notation to apply when comparing to the interval set.</param>
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
    public static int CompareMember<T>(T value, T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.IComparable<T>
    {
      AssertValid(minValue, maxValue, intervalNotation);

      var vcmpmin = value.CompareTo(minValue);
      var vcmpmax = value.CompareTo(maxValue);

      if (intervalNotation == IntervalNotation.Closed)
        return vcmpmin < 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      else if (intervalNotation == IntervalNotation.HalfOpenLeft)
        return vcmpmin <= 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      else if (intervalNotation == IntervalNotation.HalfOpenRight)
        return vcmpmin < 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      else if (intervalNotation == IntervalNotation.Open)
        return vcmpmin <= 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      else
        throw new NotImplementedException(intervalNotation.ToString());
    }

    public static Interval<T> Create<T>(T minValue, T maxValue)
      where T : System.IComparable<T>
      => new(minValue, maxValue);

    /// <summary>
    /// <para>Gets a new min/max interval (depending on type <typeparamref name="T"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
    /// <para>If <typeparamref name="T"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1) and (<paramref name="maxValue"/> - 1).</para>
    /// <para>If <typeparamref name="T"/> is a floating point value, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + epsilon) and (<paramref name="maxValue"/> - epsilon). In this context epsilon is a value that makes the original value and the new value not equal.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="intervalNotation"></param>
    /// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static (T minValue, T maxValue) GetExtent<T>(T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed, int magnitude = 1)
      where T : System.Numerics.INumber<T>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(magnitude);

      AssertValid(minValue, maxValue, intervalNotation);

      if (intervalNotation != IntervalNotation.Closed)
      {
        while (--magnitude >= 0)
        {
          if (intervalNotation == IntervalNotation.Open || intervalNotation == IntervalNotation.HalfOpenLeft)
            minValue = minValue.GetSupremum();

          if (intervalNotation == IntervalNotation.Open || intervalNotation == IntervalNotation.HalfOpenRight)
            maxValue = maxValue.GetInfimum();
        }

        AssertValid(minValue, maxValue, IntervalNotation.Closed, "minExtent/maxExtent");
      }

      return (minValue, maxValue);
    }

    /// <summary>
    /// <para>Gets a new min/max interval, using the <paramref name="intervalNotation"/> <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minMargin"></param>
    /// <param name="maxMargin"></param>
    /// <param name="intervalNotation"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static (T minValue, T maxValue) GetMargin<T>(T minValue, T maxValue, T minMargin, T maxMargin, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(minMargin);
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxMargin);

      AssertValid(minValue, maxValue);

      if (intervalNotation != IntervalNotation.Closed)
      {
        if (intervalNotation == IntervalNotation.Open || intervalNotation == IntervalNotation.HalfOpenLeft)
          minValue += minMargin;

        if (intervalNotation == IntervalNotation.Open || intervalNotation == IntervalNotation.HalfOpenRight)
          maxValue -= maxMargin;

        AssertValid(minValue, maxValue, IntervalNotation.Closed, "minMargin/maxMargin");
      }

      return (minValue, maxValue);
    }

    /// <summary>
    /// <para>Determines whether the interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a degenerate interval, i.e. the interval consists of only a single value.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="intervalNotation"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <remarks>If <paramref name="intervalNotation"/> is <see cref="IntervalNotation.Closed"/> and <paramref name="minValue"/> equals <paramref name="maxValue"/>, then it is a degenerate interval.</remarks>
    public static bool IsDegenerate<T>(T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.IComparable<T>
    {
      AssertValid(minValue, maxValue, intervalNotation);

      return intervalNotation == IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0;
    }

    /// <summary>
    /// <para>Determines whether the interval <paramref name="minValue"/>..<paramref name="maxValue"/> represents the empty set.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="intervalNotation"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <remarks>If <paramref name="minValue"/> equals <paramref name="maxValue"/> and <paramref name="intervalNotation"/> notation is not closed, or if <paramref name="minValue"/> is greater than <paramref name="maxValue"/>, then the interval is the empty set.</remarks>
    public static bool IsEmptySet<T>(T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.IComparable<T>
    {
      AssertValid(minValue, maxValue, intervalNotation);

      return (intervalNotation != IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0) // If minValue equals maxValue, all but the closed notation represents the empty set.
      || (minValue.CompareTo(maxValue) > 0 || maxValue.CompareTo(minValue) < 0); // If minValue is greater than maxValue, all four notations are usually taken to represent the empty set.
    }

    /// <summary>
    /// <para>Determines if a <paramref name="value"/> is a member of an interval <paramref name="minValue"/>..<paramref name="maxValue"/> with the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value to verify.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <param name="intervalNotation">The interval notation to apply when determining membership of the interval set.</param>
    /// <returns>Whether the <paramref name="value"/> is a member of the interval.</returns>
    public static bool IsMember<T>(T value, T minValue, T maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.IComparable<T>
    {
      try
      {
        return CompareMember(value, minValue, maxValue, intervalNotation) == 0;
      }
      catch { }

      return false;
    }

    /// <summary>
    /// <para>Determines whether an interval itself is valid.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool IsValid<T>(IntervalNotation source, T minValue, T maxValue)
      where T : System.IComparable<T>
    {
      if (source == IntervalNotation.Closed)
        return minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) >= 0;
      else if (source == IntervalNotation.HalfOpenRight)
        return minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) > 0;
      else if (source == IntervalNotation.HalfOpenLeft)
        return minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) >= 0;
      else if (source == IntervalNotation.Open)
        return minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) > 0;
      else
        throw new NotImplementedException(source.ToString());
    }

    /// <summary>
    /// <para>Creates a string representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) of the interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string ToIntervalNotationString<T>(T minValue, T maxValue, IntervalNotation source = IntervalNotation.Closed, string? format = null, System.IFormatProvider? provider = null)
      where T : System.IComparable<T>
    {
      format = $"{{0{(format is null ? string.Empty : $":{format}")}}}";

      return $"{source.ToIntervalNotationCharLeft()}{string.Format(provider, format, minValue)}, {string.Format(provider, format, maxValue)}{source.ToIntervalNotationCharRight()}";
    }

    public static TNumber Wrap<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      (minValue, maxValue) = Interval.GetExtent(minValue, maxValue, intervalNotation);

      if (value > maxValue)
        return minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One));

      if (value < minValue)
        return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One));

      return value;

      //// FROM Compare above!

      //var vcmpmin = value.CompareTo(minValue);
      //var vcmpmax = value.CompareTo(maxValue);

      //if (intervalNotation == IntervalNotation.Closed)
      //  return vcmpmin < 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      //else if (intervalNotation == IntervalNotation.HalfOpenLeft)
      //  return vcmpmin <= 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      //else if (intervalNotation == IntervalNotation.HalfOpenRight)
      //  return vcmpmin < 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      //else if (intervalNotation == IntervalNotation.Open)
      //  return vcmpmin <= 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      //else
      //  throw new NotImplementedException(intervalNotation.ToString());
    }
  }

  /// <summary>
  /// <para>Represents a closed interval or value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
  /// <para>An interval is displayed using standard mathematical interval notation.</para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public readonly record struct Interval<T>
    : System.IComparable, System.IComparable<Interval<T>>, System.IFormattable
    where T : System.IComparable<T>
  {
    /// <remarks>The default interval is representing a degenerate interval with the default value of <typeparamref name="T"/>, i.e. zero for standard numerical types, e.g. int, double, BigInteger, etc.</remarks>
    public readonly static Interval<T> Default;

    private readonly T m_maxValue;
    private readonly T m_minValue;

    public Interval(T minValue, T maxValue)
      => (m_minValue, m_maxValue) = Interval.AssertValid(minValue, maxValue);

    public void Deconstruct(out T minValue, out T maxValue)
    {
      minValue = m_minValue;
      maxValue = m_maxValue;
    }

    public T MaxValue => m_maxValue;
    public T MinValue => m_minValue;

    #region Static methods

    /// <summary>
    /// <para>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> Intersect(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
        list.Add(new Interval<T>(LargerMinimum(a, b), SmallerMaximum(a, b)));

      return list;
    }

    /// <summary>
    /// <para>Returns whether a and b are overlapping.</para>
    /// </summary>
    public static bool IsOverlapping(Interval<T> a, Interval<T> b)
      => a.m_minValue.CompareTo(b.m_maxValue) < 0 && b.m_minValue.CompareTo(a.m_maxValue) < 0;

    /// <summary>
    /// <para>Returns the maximum high value of a and b.</para>
    /// </summary>
    public static T LargerMaximum(Interval<T> a, Interval<T> b)
      => a.m_maxValue.CompareTo(b.m_maxValue) >= 0 ? a.m_maxValue : b.m_maxValue;

    /// <summary>
    /// <para>Returns the maximum low value of a and b.</para>
    /// </summary>
    public static T LargerMinimum(Interval<T> a, Interval<T> b)
      => a.m_minValue.CompareTo(b.m_minValue) >= 0 ? a.m_minValue : b.m_minValue;

    /// <summary>
    /// <para>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> LeftDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_minValue.CompareTo(b.m_minValue) < 0) list.Add(new Interval<T>(a.m_minValue, b.m_minValue));
        if (a.m_maxValue.CompareTo(b.m_maxValue) > 0) list.Add(new Interval<T>(b.m_maxValue, a.m_maxValue));
      }
      else
        list.Add(a);

      return list;
    }

    /// <summary>
    /// <para>Right different is the set of all elements that are members of B, but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> RightDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_minValue.CompareTo(a.m_minValue) < 0) list.Add(new Interval<T>(b.m_minValue, a.m_minValue));
        if (b.m_maxValue.CompareTo(a.m_maxValue) > 0) list.Add(new Interval<T>(a.m_maxValue, b.m_maxValue));
      }
      else
        list.Add(b);

      return list;
    }

    /// <summary>
    /// <para>Returns the minimum high value of a and b.</para>
    /// </summary>
    public static T SmallerMaximum(Interval<T> a, Interval<T> b)
      => a.m_maxValue.CompareTo(b.m_maxValue) <= 0 ? a.m_maxValue : b.m_maxValue;

    /// <summary>
    /// <para>Returns the minimum low value of a and b.</para>
    /// </summary>
    public static T SmallerMinimum(Interval<T> a, Interval<T> b)
      => a.m_minValue.CompareTo(b.m_minValue) <= 0 ? a.m_minValue : b.m_minValue;

    /// <summary>
    /// <para>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> SymmetricDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_minValue.CompareTo(b.m_minValue) < 0) list.Add(new Interval<T>(a.m_minValue, b.m_minValue));
        if (b.m_minValue.CompareTo(a.m_minValue) < 0) list.Add(new Interval<T>(b.m_minValue, a.m_minValue));
        if (a.m_maxValue.CompareTo(b.m_maxValue) < 0) list.Add(new Interval<T>(a.m_maxValue, b.m_maxValue));
        if (b.m_maxValue.CompareTo(a.m_maxValue) < 0) list.Add(new Interval<T>(b.m_maxValue, a.m_maxValue));
      }
      else
      {
        list.Add(a);
        list.Add(b);
      }

      return list;
    }

    /// <summary>
    /// <para>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> Union(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else
        list.Add(new Interval<T>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator Interval<T>(System.ValueTuple<T, T> vt) => new(vt.Item1, vt.Item2);
    public static explicit operator System.ValueTuple<T, T>(Interval<T> vr) => (vr.m_minValue, vr.m_maxValue);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? obj)
      => obj is Interval<T> o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Interval<T> other)
      => (m_minValue.CompareTo(other.m_minValue) is var cmin && cmin != 0) ? cmin : (m_maxValue.CompareTo(other.m_maxValue) is var cmax && cmax != 0) ? cmax : 0;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => Interval.ToIntervalNotationString(m_minValue, m_maxValue, IntervalNotation.Closed, format, formatProvider);

    #endregion Implemented interfaces

    public override string ToString()
      => ToString(null, null);
  }
}
