namespace Flux.Units
{
  /// <summary>
  /// <para>Irradiance, unit of watt per square meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Irradiance"/>
  /// </summary>
  public readonly record struct Irradiance
    : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, ISiUnitValueQuantifiable<double, IrradianceUnit>
  {
    private readonly double m_value;

    public Irradiance(double value, IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public Irradiance(MetricPrefix prefix, double wattPerSquareMeter) => m_value = prefix.ChangePrefix(wattPerSquareMeter, MetricPrefix.Unprefixed);

    public Irradiance(Power power, Area area) : this(power.Value / area.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Irradiance a, Irradiance b) => a.CompareTo(b) < 0;
    public static bool operator >(Irradiance a, Irradiance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Irradiance a, Irradiance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Irradiance a, Irradiance b) => a.CompareTo(b) >= 0;

    public static Irradiance operator -(Irradiance v) => new(-v.m_value);
    public static Irradiance operator *(Irradiance a, Irradiance b) => new(a.m_value * b.m_value);
    public static Irradiance operator /(Irradiance a, Irradiance b) => new(a.m_value / b.m_value);
    public static Irradiance operator %(Irradiance a, Irradiance b) => new(a.m_value % b.m_value);
    public static Irradiance operator +(Irradiance a, Irradiance b) => new(a.m_value + b.m_value);
    public static Irradiance operator -(Irradiance a, Irradiance b) => new(a.m_value - b.m_value);
    public static Irradiance operator *(Irradiance a, double b) => new(a.m_value * b);
    public static Irradiance operator /(Irradiance a, double b) => new(a.m_value / b);
    public static Irradiance operator %(Irradiance a, double b) => new(a.m_value % b);
    public static Irradiance operator +(Irradiance a, double b) => new(a.m_value + b);
    public static Irradiance operator -(Irradiance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Irradiance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Irradiance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(IrradianceUnit.WattPerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + IrradianceUnit.WattPerSquareMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(IrradianceUnit unit, double value)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(IrradianceUnit unit, double value)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, IrradianceUnit from, IrradianceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(IrradianceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Irradiance.Value"/> property is in <see cref="IrradianceUnit.WattPerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
