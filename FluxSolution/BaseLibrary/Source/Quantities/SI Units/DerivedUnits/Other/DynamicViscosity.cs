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

    public DynamicViscosity(double value, DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond)
      => m_value = unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueString(DynamicViscosityUnit.PascalSecond, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="DynamicViscosity.Value"/> property is in <see cref="DynamicViscosityUnit.PascalSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitSymbol(DynamicViscosityUnit unit, bool preferUnicode, bool useFullName)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.DynamicViscosityUnit.PascalSecond => preferUnicode ? "Pa\u22C5s" : "Pa·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(DynamicViscosityUnit unit)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueString(DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
      return sb.ToString();
    }

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
