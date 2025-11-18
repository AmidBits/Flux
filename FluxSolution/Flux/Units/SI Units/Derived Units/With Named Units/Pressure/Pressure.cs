namespace Flux.Units
{
  /// <summary>
  /// <para>Pressure, unit of Pascal. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Pressure"/></para>
  /// </summary>
  public readonly record struct Pressure
    : System.IComparable, System.IComparable<Pressure>, System.IFormattable, ISiUnitValueQuantifiable<double, PressureUnit>
  {
    private readonly double m_value;

    public Pressure(double value, PressureUnit unit = PressureUnit.Pascal) => m_value = ConvertFromUnit(unit, value);

    public Pressure(MetricPrefix prefix, double pascal) => m_value = prefix.ConvertPrefix(pascal, MetricPrefix.Unprefixed);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Pressure a, Pressure b) => a.CompareTo(b) < 0;
    public static bool operator >(Pressure a, Pressure b) => a.CompareTo(b) > 0;
    public static bool operator <=(Pressure a, Pressure b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Pressure a, Pressure b) => a.CompareTo(b) >= 0;

    public static Pressure operator -(Pressure v) => new(-v.m_value);
    public static Pressure operator *(Pressure a, Pressure b) => new(a.m_value * b.m_value);
    public static Pressure operator /(Pressure a, Pressure b) => new(a.m_value / b.m_value);
    public static Pressure operator %(Pressure a, Pressure b) => new(a.m_value % b.m_value);
    public static Pressure operator +(Pressure a, Pressure b) => new(a.m_value + b.m_value);
    public static Pressure operator -(Pressure a, Pressure b) => new(a.m_value - b.m_value);
    public static Pressure operator *(Pressure a, double b) => new(a.m_value * b);
    public static Pressure operator /(Pressure a, double b) => new(a.m_value / b);
    public static Pressure operator %(Pressure a, double b) => new(a.m_value % b);
    public static Pressure operator +(Pressure a, double b) => new(a.m_value + b);
    public static Pressure operator -(Pressure a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Pressure o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Pressure other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + PressureUnit.Pascal.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PressureUnit unit, double value)
      => unit switch
      {
        PressureUnit.Pascal => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(PressureUnit unit, double value)
      => unit switch
      {
        PressureUnit.Pascal => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, PressureUnit from, PressureUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(PressureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PressureUnit unit = PressureUnit.Pascal, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Pressure.Value"/> property is in <see cref="PressureUnit.Pascal"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
