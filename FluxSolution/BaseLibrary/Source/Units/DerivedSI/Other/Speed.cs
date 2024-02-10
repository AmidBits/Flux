namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SpeedUnit unit, Units.TextOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.SpeedUnit.FootPerSecond => "ft/s",
        Units.SpeedUnit.KilometerPerHour => "km/h",
        Units.SpeedUnit.Knot => options.PreferUnicode ? "\u33CF" : "knot",
        Units.SpeedUnit.MeterPerSecond => options.PreferUnicode ? "\u33A7" : "m/s",
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
      MilePerHour,
    }

    /// <summary>Speed, unit of meters per second.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Speed"/>
    public readonly record struct Speed
      : System.IComparable, System.IComparable<Speed>, System.IFormattable, IUnitValueQuantifiable<double, SpeedUnit>
    {
      /// <summary>The speed of light in vacuum.</summary>
      public static Speed SpeedOfLight => new(299792458);
      /// <summary>The speed of sound in air.</summary>
      public static Speed SpeedOfSound => new(343);

      private readonly double m_value;

      public Speed(double value, SpeedUnit unit = SpeedUnit.MeterPerSecond)
        => m_value = unit switch
        {
          SpeedUnit.FootPerSecond => value * (381.0 / 1250.0),
          SpeedUnit.KilometerPerHour => value * (5.0 / 18.0),
          SpeedUnit.Knot => value * (1852.0 / 3600.0),
          SpeedUnit.MeterPerSecond => value,
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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default) => ToUnitValueString(SpeedUnit.MeterPerSecond, options);

      /// <summary>
      ///  <para>The unit of the <see cref="Speed.Value"/> property is in <see cref="SpeedUnit.MeterPerSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(SpeedUnit unit)
        => unit switch
        {
          SpeedUnit.FootPerSecond => m_value * (1250.0 / 381.0),
          SpeedUnit.KilometerPerHour => m_value * (18.0 / 5.0),
          SpeedUnit.Knot => m_value * (3600.0 / 1852.0),
          SpeedUnit.MeterPerSecond => m_value,
          SpeedUnit.MilePerHour => m_value * (3125.0 / 1397.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SpeedUnit unit, TextOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
