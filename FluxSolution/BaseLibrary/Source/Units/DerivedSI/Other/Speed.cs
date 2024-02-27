namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SpeedUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.SpeedUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",
        Units.SpeedUnit.FootPerSecond => "ft/s",
        Units.SpeedUnit.KilometerPerHour => "km/h",
        Units.SpeedUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        Units.SpeedUnit.Mach => "Mach",
        Units.SpeedUnit.MilePerHour => "mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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

      #region Static methods
      /// <summary>Create a new Speed instance representing phase velocity from the specified frequency and wavelength.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Phase_velocity"/>
      /// <param name="frequency"></param>
      /// <param name="wavelength"></param>
      public static Speed ComputePhaseVelocity(Frequency frequency, Length wavelength)
        => new(frequency.Value * wavelength.Value);

      /// <summary>Creates a new Speed instance from the specified length and time.</summary>
      /// <param name="length"></param>
      /// <param name="time"></param>
      public static Speed From(Length length, Time time)
        => new(length.Value / time.Value);

      /// <summary>Creates a new <see cref="Speed">tangential/linear speed</see> instance from the specified <see cref="AngularFrequency"/> and <see cref="Length">Radius</see>.</summary>
      /// <param name="angularVelocity"></param>
      /// <param name="radius"></param>
      public static Speed From(AngularFrequency angularVelocity, Length radius)
        => new(angularVelocity.Value * radius.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Speed v) => v.m_value;
      public static explicit operator Speed(double v) => new(v);

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
        => ToUnitValueString(SpeedUnit.MeterPerSecond, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      ///  <para>The unit of the <see cref="Speed.Value"/> property is in <see cref="SpeedUnit.MeterPerSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
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

      public string ToUnitValueString(SpeedUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        if (unit == SpeedUnit.Mach)
        {
          sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
          sb.Append(options.UnitSpacing.ToSpacingString());
        }
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        if (unit != SpeedUnit.Mach)
        {
          sb.Append(options.UnitSpacing.ToSpacingString());
          sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        }
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
