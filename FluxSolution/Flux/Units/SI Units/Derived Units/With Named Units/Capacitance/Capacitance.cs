namespace Flux.Units
{
  /// <summary>
  /// <para>Electrical capacitance, unit of Farad.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Capacitance"/></para>
  /// </summary>
  public readonly record struct Capacitance
    : System.IComparable, System.IComparable<Capacitance>, System.IFormattable, ISiUnitValueQuantifiable<double, CapacitanceUnit>
  {
    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = CapacitanceUnit.Farad) => m_value = ConvertFromUnit(unit, value);

    public Capacitance(MetricPrefix prefix, double farad) => m_value = prefix.ConvertPrefix(farad, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Capacitance a, Capacitance b) => a.CompareTo(b) < 0;
    public static bool operator >(Capacitance a, Capacitance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Capacitance a, Capacitance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Capacitance a, Capacitance b) => a.CompareTo(b) >= 0;

    public static Capacitance operator -(Capacitance v) => new(-v.m_value);
    public static Capacitance operator *(Capacitance a, Capacitance b) => new(a.m_value * b.m_value);
    public static Capacitance operator /(Capacitance a, Capacitance b) => new(a.m_value / b.m_value);
    public static Capacitance operator %(Capacitance a, Capacitance b) => new(a.m_value % b.m_value);
    public static Capacitance operator +(Capacitance a, Capacitance b) => new(a.m_value + b.m_value);
    public static Capacitance operator -(Capacitance a, Capacitance b) => new(a.m_value - b.m_value);
    public static Capacitance operator *(Capacitance a, double b) => new(a.m_value * b);
    public static Capacitance operator /(Capacitance a, double b) => new(a.m_value / b);
    public static Capacitance operator %(Capacitance a, double b) => new(a.m_value % b);
    public static Capacitance operator +(Capacitance a, double b) => new(a.m_value + b);
    public static Capacitance operator -(Capacitance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Capacitance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Capacitance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + CapacitanceUnit.Farad.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, CapacitanceUnit from, CapacitanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(CapacitanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
