namespace Flux.Units
{
  /// <summary>
  /// <para>Dynamic viscosity, unit of Pascal second.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Dynamic_viscosity"/></para>
  /// </summary>
  public readonly record struct DynamicViscosity
    : System.IComparable, System.IComparable<DynamicViscosity>, System.IFormattable, ISiUnitValueQuantifiable<double, DynamicViscosityUnit>
  {
    private readonly double m_value;

    public DynamicViscosity(double value, DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond) => m_value = ConvertToUnit(unit, value);

    public DynamicViscosity(MetricPrefix prefix, double pascalSecond) => m_value = prefix.ChangePrefix(pascalSecond, MetricPrefix.Unprefixed);

    public DynamicViscosity(Pressure pressure, Time time) : this(pressure.Value * time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) < 0;
    public static bool operator >(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) > 0;
    public static bool operator <=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) >= 0;

    public static DynamicViscosity operator -(DynamicViscosity v) => new(-v.m_value);
    public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b) => new(a.m_value * b.m_value);
    public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b) => new(a.m_value / b.m_value);
    public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b) => new(a.m_value % b.m_value);
    public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b) => new(a.m_value + b.m_value);
    public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b) => new(a.m_value - b.m_value);
    public static DynamicViscosity operator *(DynamicViscosity a, double b) => new(a.m_value * b);
    public static DynamicViscosity operator /(DynamicViscosity a, double b) => new(a.m_value / b);
    public static DynamicViscosity operator %(DynamicViscosity a, double b) => new(a.m_value % b);
    public static DynamicViscosity operator +(DynamicViscosity a, double b) => new(a.m_value + b);
    public static DynamicViscosity operator -(DynamicViscosity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is DynamicViscosity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(DynamicViscosity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(DynamicViscosityUnit.PascalSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + DynamicViscosityUnit.PascalSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(DynamicViscosityUnit unit, double value)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(DynamicViscosityUnit unit, double value)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, DynamicViscosityUnit from, DynamicViscosityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(DynamicViscosityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(DynamicViscosityUnit unit = DynamicViscosityUnit.PascalSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="DynamicViscosity.Value"/> property is in <see cref="DynamicViscosityUnit.PascalSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
