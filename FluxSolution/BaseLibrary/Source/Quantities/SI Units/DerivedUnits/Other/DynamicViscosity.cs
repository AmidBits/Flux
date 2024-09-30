namespace Flux.Quantities
{
  public enum DynamicViscosityUnit
  {
    /// <summary>This is the default unit for <see cref="DynamicViscosity"/>.</summary>
    PascalSecond,
  }

  /// <summary>Dynamic viscosity, unit of Pascal second.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Dynamic_viscosity"/>
  public readonly record struct DynamicViscosity
    : System.IComparable, System.IComparable<DynamicViscosity>, System.IFormattable, IUnitValueQuantifiable<double, DynamicViscosityUnit>
  {
    private readonly double m_value;

    public DynamicViscosity(double value, DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond) => m_value = ConvertToUnit(unit, m_value);

    public DynamicViscosity(Pressure pressure, Time time) : this(pressure.Value * time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) < 0;
    public static bool operator <=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) <= 0;
    public static bool operator >(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) > 0;
    public static bool operator >=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) >= 0;

    public static DynamicViscosity operator -(DynamicViscosity v) => new(-v.m_value);
    public static DynamicViscosity operator +(DynamicViscosity a, double b) => new(a.m_value + b);
    public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b) => a + b.m_value;
    public static DynamicViscosity operator /(DynamicViscosity a, double b) => new(a.m_value / b);
    public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b) => a / b.m_value;
    public static DynamicViscosity operator *(DynamicViscosity a, double b) => new(a.m_value * b);
    public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b) => a * b.m_value;
    public static DynamicViscosity operator %(DynamicViscosity a, double b) => new(a.m_value % b);
    public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b) => a % b.m_value;
    public static DynamicViscosity operator -(DynamicViscosity a, double b) => new(a.m_value - b);
    public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is DynamicViscosity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(DynamicViscosity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(DynamicViscosityUnit.PascalSecond, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="DynamicViscosity.Value"/> property is in <see cref="DynamicViscosityUnit.PascalSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(DynamicViscosityUnit unit, double value)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(DynamicViscosityUnit unit, double value)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(DynamicViscosityUnit unit)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(DynamicViscosityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(DynamicViscosityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.DynamicViscosityUnit.PascalSecond => preferUnicode ? "Pa\u22C5s" : "Pa·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(DynamicViscosityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
