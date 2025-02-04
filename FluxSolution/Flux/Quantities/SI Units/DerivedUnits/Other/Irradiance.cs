namespace Flux.Quantities
{
  public enum IrradianceUnit
  {
    /// <summary>This is the default unit for <see cref="Irradiance"/>.</summary>
    WattPerSquareMeter,
  }

  /// <summary>
  /// <para>Irradiance, unit of watt per square meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Irradiance"/>
  /// </summary>
  public readonly record struct Irradiance
    : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, ISiUnitValueQuantifiable<double, IrradianceUnit>
  {
    private readonly double m_value;

    public Irradiance(double value, IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public Irradiance(MetricPrefix prefix, double wattPerSquareMeter) => m_value = prefix.ConvertTo(wattPerSquareMeter, MetricPrefix.Unprefixed);

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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(IrradianceUnit.WattPerSquareMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(IrradianceUnit.WattPerSquareMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(IrradianceUnit unit, double value)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(IrradianceUnit unit, double value)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, IrradianceUnit from, IrradianceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(IrradianceUnit unit)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(IrradianceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(IrradianceUnit unit, bool preferUnicode)
      => unit switch
      {
        IrradianceUnit.WattPerSquareMeter => "W/m²",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(IrradianceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
