namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.SpeedUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.SpeedUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",
        Quantities.SpeedUnit.FootPerSecond => "ft/s",
        Quantities.SpeedUnit.KilometerPerHour => "km/h",
        Quantities.SpeedUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        Quantities.SpeedUnit.Mach => "Mach",
        Quantities.SpeedUnit.MilePerHour => "mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
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
      : System.IComparable, System.IComparable<Speed>, System.IFormattable, IUnitValueQuantifiable<double, SpeedUnit>
    {
      /// <summary>The speed of light in vacuum (symbol c).</summary>
      public static Speed SpeedOfLight => new(299792458);

      /// <summary>The speed of sound in dry air at sea-level pressure and 20 °C.</summary>
      public static Speed SpeedOfSound => new(343);

      private readonly double m_value;

      public Speed(double value, SpeedUnit unit = SpeedUnit.MeterPerSecond)
        => m_value = unit switch
        {
          SpeedUnit.MeterPerSecond => value,
          SpeedUnit.FootPerSecond => value * (381.0 / 1250.0),
          SpeedUnit.KilometerPerHour => value * (5.0 / 18.0),
          SpeedUnit.Knot => value * (1852.0 / 3600.0),
          SpeedUnit.Mach => value * SpeedOfSound.Value,
          SpeedUnit.MilePerHour => value * (1397.0 / 3125.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

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
        => ToUnitValueString(SpeedUnit.MeterPerSecond, format, formatProvider);

      // IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(SpeedUnit.MeterPerSecond.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      ///  <para>The unit of the <see cref="Speed.Value"/> property is in <see cref="SpeedUnit.MeterPerSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(SpeedUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(SpeedUnit unit)
        => unit switch
        {
          SpeedUnit.MeterPerSecond => m_value,
          SpeedUnit.FootPerSecond => m_value * (1250.0 / 381.0),
          SpeedUnit.KilometerPerHour => m_value * (18.0 / 5.0),
          SpeedUnit.Knot => m_value * (3600.0 / 1852.0),
          SpeedUnit.Mach => m_value / SpeedOfSound.Value,
          SpeedUnit.MilePerHour => m_value * (3125.0 / 1397.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SpeedUnit unit = SpeedUnit.MeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        if (unit == SpeedUnit.Mach)
        {
          sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
          sb.Append(unitSpacing.ToSpacingString());
        }
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        if (unit != SpeedUnit.Mach)
        {
          sb.Append(unitSpacing.ToSpacingString());
          sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        }
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
