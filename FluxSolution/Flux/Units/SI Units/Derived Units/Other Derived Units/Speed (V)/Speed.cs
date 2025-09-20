namespace Flux.Units
{
  /// <summary>
  /// <para>Speed, unit of meters per second, is the magnitude of velocity (a vector).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Speed"/></para>
  /// </summary>
  public readonly record struct Speed
    : System.IComparable, System.IComparable<Speed>, System.IFormattable, ISiUnitValueQuantifiable<double, SpeedUnit>
  {
    /// <summary>
    /// <para>The speed of light in vacuum (symbol c), in (base) unit of meter per second.</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Speed_of_light"/></para>
    /// </summary>
    public const double SpeedOfLight = 299792458;

    /// <summary>
    /// <para>The speed of sound in dry air at sea-level pressure and 20 °C, in (base) unit of meter per second.</para>
    /// </summary>
    public const double SpeedOfSound = 343;

    private readonly double m_value;

    public Speed(double value, SpeedUnit unit = SpeedUnit.MeterPerSecond) => m_value = ConvertFromUnit(unit, value);

    public Speed(MetricPrefix prefix, double meterPerSecond) => m_value = prefix.ChangePrefix(meterPerSecond, MetricPrefix.Unprefixed);

    /// <summary>Create a new instance representing phase velocity from the specified frequency and wavelength.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_velocity"/>
    /// <param name="frequency"></param>
    /// <param name="wavelength"></param>
    public Speed(Frequency frequency, Length wavelength) : this(frequency.Value * wavelength.Value) { }

    /// <summary>Creates a new instance from the specified distance and time.</summary>
    /// <param name="distance"></param>
    /// <param name="time"></param>
    public Speed(Length distance, Time time) : this(distance.Value / time.Value) { }

    /// <summary>Creates a new instance (tangential/linear speed) from the specified <see cref="AngularFrequency"/> and <see cref="Length">Radius</see>.</summary>
    /// <param name="angularVelocity"></param>
    /// <param name="radius"></param>
    public Speed(AngularFrequency angularVelocity, Length radius) : this(angularVelocity.Value * radius.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Speed a, Speed b) => a.CompareTo(b) < 0;
    public static bool operator >(Speed a, Speed b) => a.CompareTo(b) > 0;
    public static bool operator <=(Speed a, Speed b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Speed a, Speed b) => a.CompareTo(b) >= 0;

    public static Speed operator -(Speed v) => new(-v.m_value);
    public static Speed operator *(Speed a, Speed b) => new(a.m_value * b.m_value);
    public static Speed operator /(Speed a, Speed b) => new(a.m_value / b.m_value);
    public static Speed operator %(Speed a, Speed b) => new(a.m_value % b.m_value);
    public static Speed operator +(Speed a, Speed b) => new(a.m_value + b.m_value);
    public static Speed operator -(Speed a, Speed b) => new(a.m_value - b.m_value);
    public static Speed operator *(Speed a, double b) => new(a.m_value * b);
    public static Speed operator /(Speed a, double b) => new(a.m_value / b);
    public static Speed operator %(Speed a, double b) => new(a.m_value % b);
    public static Speed operator +(Speed a, double b) => new(a.m_value + b);
    public static Speed operator -(Speed a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Speed o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Speed other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitString(SpeedUnit.MeterPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + SpeedUnit.MeterPerSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(SpeedUnit unit, double value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(SpeedUnit unit, double value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, SpeedUnit from, SpeedUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(SpeedUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var sm = new SpanMaker<char>();

      var value = GetUnitValue(unit);

      if (unit == SpeedUnit.Mach)
      {
        sm.Append(fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
        sm.Append(spacing.ToSpacingString());
      }

      sm.Append(GetUnitValue(unit).ToString(format, formatProvider));

      if (unit != SpeedUnit.Mach)
      {
        sm.Append(spacing.ToSpacingString());
        sm.Append(fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
      }

      return sm.ToString();
    }

    //public string ToUnitValueNameString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //{
    //  var sb = new System.Text.StringBuilder();
    //  if (unit == SpeedUnit.Mach)
    //  {
    //    sb.Append(GetUnitName(unit, preferPlural));
    //    sb.Append(unitSpacing.ToSpacingString());
    //  }
    //  sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
    //  if (unit != SpeedUnit.Mach)
    //  {
    //    sb.Append(unitSpacing.ToSpacingString());
    //    sb.Append(GetUnitName(unit, preferPlural));
    //  }
    //  return sb.ToString();
    //}

    //public string ToUnitValueSymbolString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //{
    //  var sb = new System.Text.StringBuilder();
    //  if (unit == SpeedUnit.Mach)
    //  {
    //    sb.Append(GetUnitSymbol(unit, preferUnicode));
    //    sb.Append(unitSpacing.ToSpacingString());
    //  }
    //  sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
    //  if (unit != SpeedUnit.Mach)
    //  {
    //    sb.Append(unitSpacing.ToSpacingString());
    //    sb.Append(GetUnitSymbol(unit, preferUnicode));
    //  }
    //  return sb.ToString();
    //}

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Speed.Value"/> property is in <see cref="SpeedUnit.MeterPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
