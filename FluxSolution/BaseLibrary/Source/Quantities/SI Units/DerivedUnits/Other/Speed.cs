namespace Flux.Quantities
{
  public enum SpeedUnit
  {
    /// <summary>This is the default unit for <see cref="Speed"/>.</summary>
    MeterPerSecond,
    FootPerSecond,
    KilometerPerHour,
    Knot,
    Mach,
    MilePerHour,
  }

  /// <summary>Speed, unit of meters per second.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Speed"/>
  public readonly record struct Speed
    : System.IComparable, System.IComparable<Speed>, System.IFormattable, ISiUnitValueQuantifiable<double, SpeedUnit>
  {
    /// <summary>The speed of light in vacuum (symbol c).</summary>
    public static Speed SpeedOfLight => new(299792458);

    /// <summary>The speed of sound in dry air at sea-level pressure and 20 °C.</summary>
    public static Speed SpeedOfSound => new(343);

    private readonly double m_value;

    public Speed(double value, SpeedUnit unit = SpeedUnit.MeterPerSecond) => m_value = ConvertFromUnit(unit, value);

    /// <summary>Create a new Speed instance representing phase velocity from the specified frequency and wavelength.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_velocity"/>
    /// <param name="frequency"></param>
    /// <param name="wavelength"></param>
    public Speed(Frequency frequency, Length wavelength) : this(frequency.Value * wavelength.Value) { }

    /// <summary>Creates a new Speed instance from the specified distance and time.</summary>
    /// <param name="distance"></param>
    /// <param name="time"></param>
    public Speed(Length distance, Time time) : this(distance.Value / time.Value) { }

    /// <summary>Creates a new <see cref="Speed">tangential/linear speed</see> instance from the specified <see cref="AngularFrequency"/> and <see cref="Length">Radius</see>.</summary>
    /// <param name="angularVelocity"></param>
    /// <param name="radius"></param>
    public Speed(AngularFrequency angularVelocity, Length radius) : this(angularVelocity.Value * radius.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Speed a, Speed b) => a.CompareTo(b) < 0;
    public static bool operator <=(Speed a, Speed b) => a.CompareTo(b) <= 0;
    public static bool operator >(Speed a, Speed b) => a.CompareTo(b) > 0;
    public static bool operator >=(Speed a, Speed b) => a.CompareTo(b) >= 0;

    public static Speed operator -(Speed v) => new(-v.m_value);
    public static Speed operator +(Speed a, double b) => new(a.m_value + b);
    public static Speed operator +(Speed a, Speed b) => a + b.m_value;
    public static Speed operator /(Speed a, double b) => new(a.m_value / b);
    public static Speed operator /(Speed a, Speed b) => a / b.m_value;
    public static Speed operator *(Speed a, double b) => new(a.m_value * b);
    public static Speed operator *(Speed a, Speed b) => a * b.m_value;
    public static Speed operator %(Speed a, double b) => new(a.m_value % b);
    public static Speed operator %(Speed a, Speed b) => a % b.m_value;
    public static Speed operator -(Speed a, double b) => new(a.m_value - b);
    public static Speed operator -(Speed a, Speed b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Speed o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Speed other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(SpeedUnit.MeterPerSecond, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Speed.Value"/> property is in <see cref="SpeedUnit.MeterPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(SpeedUnit.MeterPerSecond, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(SpeedUnit.MeterPerSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(SpeedUnit unit, double value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(SpeedUnit unit, double value)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, SpeedUnit from, SpeedUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(SpeedUnit unit)
      => unit switch
      {
        SpeedUnit.MeterPerSecond => 1,

        SpeedUnit.FootPerSecond => (381.0 / 1250.0),
        SpeedUnit.KilometerPerHour => (5.0 / 18.0),
        SpeedUnit.Knot => (1852.0 / 3600.0),
        SpeedUnit.Mach => SpeedOfSound.Value,
        SpeedUnit.MilePerHour => (1397.0 / 3125.0),

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(SpeedUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(SpeedUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.SpeedUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",
        Quantities.SpeedUnit.FootPerSecond => "ft/s",
        Quantities.SpeedUnit.KilometerPerHour => "km/h",
        Quantities.SpeedUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        Quantities.SpeedUnit.Mach => "Mach",
        Quantities.SpeedUnit.MilePerHour => "mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SpeedUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var sb = new System.Text.StringBuilder();
      if (unit == SpeedUnit.Mach)
      {
        sb.Append(fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));
        sb.Append(UnicodeSpacing.ThinSpace.ToSpacingString());
      }
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      if (unit != SpeedUnit.Mach)
      {
        sb.Append(UnicodeSpacing.ThinSpace.ToSpacingString());
        sb.Append(fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));
      }
      return sb.ToString();
    }

    public string ToUnitValueNameString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    {
      var sb = new System.Text.StringBuilder();
      if (unit == SpeedUnit.Mach)
      {
        sb.Append(GetUnitName(unit, preferPlural));
        sb.Append(unitSpacing.ToSpacingString());
      }
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      if (unit != SpeedUnit.Mach)
      {
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitName(unit, preferPlural));
      }
      return sb.ToString();
    }

    public string ToUnitValueSymbolString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    {
      var sb = new System.Text.StringBuilder();
      if (unit == SpeedUnit.Mach)
      {
        sb.Append(GetUnitSymbol(unit, preferUnicode));
        sb.Append(unitSpacing.ToSpacingString());
      }
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      if (unit != SpeedUnit.Mach)
      {
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode));
      }
      return sb.ToString();
    }

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
