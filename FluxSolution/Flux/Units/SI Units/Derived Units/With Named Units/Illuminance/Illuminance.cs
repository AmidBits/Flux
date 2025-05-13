namespace Flux.Units
{
  /// <summary>
  /// <para>Illuminance, unit of lux.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Illuminance"/></para>
  /// </summary>
  public readonly record struct Illuminance
    : System.IComparable, System.IComparable<Illuminance>, System.IFormattable, ISiUnitValueQuantifiable<double, IlluminanceUnit>
  {
    private readonly double m_value;

    public Illuminance(double value, IlluminanceUnit unit = IlluminanceUnit.Lux) => m_value = ConvertFromUnit(unit, value);

    public Illuminance(MetricPrefix prefix, double lux) => m_value = prefix.ChangePrefix(lux, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Illuminance a, Illuminance b) => a.CompareTo(b) < 0;
    public static bool operator >(Illuminance a, Illuminance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Illuminance a, Illuminance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Illuminance a, Illuminance b) => a.CompareTo(b) >= 0;

    public static Illuminance operator -(Illuminance v) => new(-v.m_value);
    public static Illuminance operator *(Illuminance a, Illuminance b) => new(a.m_value * b.m_value);
    public static Illuminance operator /(Illuminance a, Illuminance b) => new(a.m_value / b.m_value);
    public static Illuminance operator %(Illuminance a, Illuminance b) => new(a.m_value % b.m_value);
    public static Illuminance operator +(Illuminance a, Illuminance b) => new(a.m_value + b.m_value);
    public static Illuminance operator -(Illuminance a, Illuminance b) => new(a.m_value - b.m_value);
    public static Illuminance operator *(Illuminance a, double b) => new(a.m_value * b);
    public static Illuminance operator /(Illuminance a, double b) => new(a.m_value / b);
    public static Illuminance operator %(Illuminance a, double b) => new(a.m_value % b);
    public static Illuminance operator +(Illuminance a, double b) => new(a.m_value + b);
    public static Illuminance operator -(Illuminance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Illuminance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Illuminance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + IlluminanceUnit.Lux.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(IlluminanceUnit unit, double value)
      => unit switch
      {
        IlluminanceUnit.Lux => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(IlluminanceUnit unit, double value)
      => unit switch
      {
        IlluminanceUnit.Lux => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, IlluminanceUnit from, IlluminanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(IlluminanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
