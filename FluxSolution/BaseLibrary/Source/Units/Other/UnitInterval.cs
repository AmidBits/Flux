//namespace Flux
//{
//  namespace Units
//  {
//    /// <summary>Unit interval, unit of rational number between 0 and 1.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Unit_interval"/>
//    public readonly record struct UnitInterval
//    : System.IComparable, System.IComparable<UnitInterval>, System.IFormattable, IQuantifiable<double>
//    {
//      private readonly double m_value;

//      public UnitInterval(double unitInterval, IntervalConstraint constraint) => m_value = AssertMember(unitInterval, constraint);
//      public UnitInterval(double unitInterval) : this(unitInterval, IntervalConstraint.Closed) { }

//      #region Static methods

//      /// <summary>Asserts that the value is a member of the unit interval (throws an exception if not).</summary>
//      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//      public static double AssertMember(double value, IntervalConstraint constraint, string? paramName = null)
//        => constraint switch
//        {
//          IntervalConstraint.Closed => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a value greater-than-or-equal-to 0 and less-than-or-equal-to 1."),
//          IntervalConstraint.HalfOpenLeft => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a value greater-than 0 and less-than-or-equal-to 1."),
//          IntervalConstraint.HalfOpenRight => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a value greater-than-or-equal-to 0 and less-than 1."),
//          IntervalConstraint.Open => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be a value greater-than 0 and less-than 1."),
//          _ => throw new NotImplementedException(),
//        };

//      /// <summary>Returns whether the value is a member of the unit interval.</summary>
//      public static bool IsMember(double value, IntervalConstraint constraint)
//        => constraint switch
//        {
//          IntervalConstraint.Closed => value >= 0 && value <= 1,
//          IntervalConstraint.HalfOpenLeft => value >= 0 && value < 1,
//          IntervalConstraint.HalfOpenRight => value > 0 && value <= 1,
//          IntervalConstraint.Open => value > 0 && value < 1,
//          _ => throw new NotImplementedException(),
//        };

//      #endregion Static methods

//      #region Overloaded operators
//      public static explicit operator double(UnitInterval v) => v.m_value;
//      public static explicit operator UnitInterval(double v) => new(v);

//      public static bool operator <(UnitInterval a, UnitInterval b) => a.CompareTo(b) < 0;
//      public static bool operator <=(UnitInterval a, UnitInterval b) => a.CompareTo(b) <= 0;
//      public static bool operator >(UnitInterval a, UnitInterval b) => a.CompareTo(b) > 0;
//      public static bool operator >=(UnitInterval a, UnitInterval b) => a.CompareTo(b) >= 0;

//      public static UnitInterval operator -(UnitInterval v) => new(-v.m_value);
//      public static UnitInterval operator +(UnitInterval a, double b) => new(a.m_value + b);
//      public static UnitInterval operator +(UnitInterval a, UnitInterval b) => a + b.m_value;
//      public static UnitInterval operator /(UnitInterval a, double b) => new(a.m_value / b);
//      public static UnitInterval operator /(UnitInterval a, UnitInterval b) => a / b.m_value;
//      public static UnitInterval operator *(UnitInterval a, double b) => new(a.m_value * b);
//      public static UnitInterval operator *(UnitInterval a, UnitInterval b) => a * b.m_value;
//      public static UnitInterval operator %(UnitInterval a, double b) => new(a.m_value % b);
//      public static UnitInterval operator %(UnitInterval a, UnitInterval b) => a % b.m_value;
//      public static UnitInterval operator -(UnitInterval a, double b) => new(a.m_value - b);
//      public static UnitInterval operator -(UnitInterval a, UnitInterval b) => a - b.m_value;
//      #endregion Overloaded operators

//      #region Implemented interfaces

//      // IComparable
//      public int CompareTo(object? other) => other is not null && other is UnitInterval o ? CompareTo(o) : -1;

//      // IComparable<>
//      public int CompareTo(UnitInterval other) => m_value.CompareTo(other.m_value);

//      // IFormattable
//      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

//      // IQuantifiable<>
//      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{m_value}";
//      public double Value { get => m_value; init => m_value = value; }

//      #endregion Implemented interfaces

//      public override string ToString() => ToQuantityString();
//    }
//  }
//}
