namespace Flux
{
  namespace Units
  {
    /// <summary>Unit interval, unit of rational number between 0 and 1.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Unit_interval"/>
    public readonly record struct UnitInterval
    : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, IQuantifiable<double>
    {
      private readonly double m_value;

      public UnitInterval(double ratio) => m_value = ratio >= 0 && ratio <= 1 ? ratio : throw new System.ArgumentOutOfRangeException(nameof(ratio));

      #region Static methods

      /// <summary>Asserts the number is a valid unit (closed) interval (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static double AssertClosed(double unitInterval, string? paramName = null)
        => IsClosed(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(unitInterval), "Must be a value greater-than-or-equal-to 0 and less-than-or-equal-to 1.");

      /// <summary>Asserts the number is a valid unit (open) interval (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static double AssertOpen(double unitInterval, string? paramName = null)
        => IsOpen(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(unitInterval), "Must be a value greater-than 0 and less-than 1.");

      /// <summary>Asserts the number is a valid unit (open) interval (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static double AssertHalfOpen0(double unitInterval, string? paramName = null)
        => IsHalfOpen0(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(unitInterval), "Must be a value greater-than 0 and less-than-or-equal-to 1.");

      /// <summary>Asserts the number is a valid unit (open) interval (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static double AssertHalfOpen1(double unitInterval, string? paramName = null)
        => IsHalfOpen1(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(unitInterval), "Must be a value greater-than-or-equal-to 0 and less-than 1.");

      /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range [0, 1], including endpoints 0 and 1.</summary>
      public static bool IsClosed(double unitInterval)
        => unitInterval >= 0 && unitInterval <= 1;

      /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range (0, 1), excluding endpoints 0 and 1.</summary>
      public static bool IsOpen(double unitInterval)
        => unitInterval > 0 && unitInterval < 1;

      /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range (0, 1], excluding endpoint 0 (greater-than 0) and including endpoint 1 (less-than-or-equal-to 1).</summary>
      public static bool IsHalfOpen0(double unitInterval)
        => unitInterval >= 0 && unitInterval < 1;

      /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range [0, 1), including endpoint 0 (greater-than-or-equal-to 0) and excluding endpoint 1 (less-than-1).</summary>
      public static bool IsHalfOpen1(double unitInterval)
        => unitInterval > 0 && unitInterval <= 1;

      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(UnitInterval v) => v.m_value;
      public static explicit operator UnitInterval(double v) => new(v);

      public static bool operator <(UnitInterval a, UnitInterval b) => a.CompareTo(b) < 0;
      public static bool operator <=(UnitInterval a, UnitInterval b) => a.CompareTo(b) <= 0;
      public static bool operator >(UnitInterval a, UnitInterval b) => a.CompareTo(b) > 0;
      public static bool operator >=(UnitInterval a, UnitInterval b) => a.CompareTo(b) >= 0;

      public static UnitInterval operator -(UnitInterval v) => new(-v.m_value);
      public static UnitInterval operator +(UnitInterval a, double b) => new(a.m_value + b);
      public static UnitInterval operator +(UnitInterval a, UnitInterval b) => a + b.m_value;
      public static UnitInterval operator /(UnitInterval a, double b) => new(a.m_value / b);
      public static UnitInterval operator /(UnitInterval a, UnitInterval b) => a / b.m_value;
      public static UnitInterval operator *(UnitInterval a, double b) => new(a.m_value * b);
      public static UnitInterval operator *(UnitInterval a, UnitInterval b) => a * b.m_value;
      public static UnitInterval operator %(UnitInterval a, double b) => new(a.m_value % b);
      public static UnitInterval operator %(UnitInterval a, UnitInterval b) => a % b.m_value;
      public static UnitInterval operator -(UnitInterval a, double b) => new(a.m_value - b);
      public static UnitInterval operator -(UnitInterval a, UnitInterval b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is UnitInterval o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(UnitInterval other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{m_value}";
      public double Value { get => m_value; init => m_value = value; }

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
