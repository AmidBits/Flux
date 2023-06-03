namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static string GetUnitString(this Units.LinearVelocityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LinearVelocityUnit.FootPerSecond => "ft/s",
        Units.LinearVelocityUnit.KilometerPerHour => "km/h",
        Units.LinearVelocityUnit.Knot => preferUnicode ? "\u33CF" : "knot",
        Units.LinearVelocityUnit.MeterPerSecond => preferUnicode ? "\u33A7" : "m/s",
        Units.LinearVelocityUnit.MilePerHour => "mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LinearVelocityUnit
    {
      MeterPerSecond, // DefaultUnit first for actual instatiation defaults.
      FootPerSecond,
      KilometerPerHour,
      Knot,
      MilePerHour,
    }

    /// <summary>Speed (a.k.a. velocity) unit of meters per second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Speed"/>
    public readonly record struct LinearVelocity
      : System.IComparable, System.IComparable<LinearVelocity>, System.IFormattable, IUnitQuantifiable<double, LinearVelocityUnit>
    {
      public const LinearVelocityUnit DefaultUnit = LinearVelocityUnit.MeterPerSecond;

      /// <summary>The speed of light in vacuum.</summary>
      public static LinearVelocity SpeedOfLight => new(299792458);
      /// <summary>The speed of sound in air.</summary>
      public static LinearVelocity SpeedOfSound => new(343);

      private readonly double m_value;

      public LinearVelocity(double value, LinearVelocityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          LinearVelocityUnit.FootPerSecond => value * (381.0 / 1250.0),
          LinearVelocityUnit.KilometerPerHour => value * (5.0 / 18.0),
          LinearVelocityUnit.Knot => value * (1852.0 / 3600.0),
          LinearVelocityUnit.MeterPerSecond => value,
          LinearVelocityUnit.MilePerHour => value * (1397.0 / 3125.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Create a new Speed instance representing phase velocity from the specified frequency and wavelength.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Phase_velocity"/>
      /// <param name="frequency"></param>
      /// <param name="wavelength"></param>
      public static LinearVelocity ComputePhaseVelocity(Frequency frequency, Length wavelength)
        => new(frequency.Value * wavelength.Value);

      /// <summary>Creates a new Speed instance from the specified length and time.</summary>
      /// <param name="length"></param>
      /// <param name="time"></param>
      public static LinearVelocity From(Length length, Time time)
        => new(length.Value / time.Value);

      /// <summary>Creates a new <see cref="LinearVelocity">tangential/linear speed</see> instance from the specified <see cref="AngularVelocity"/> and <see cref="Length">Radius</see>.</summary>
      /// <param name="angularVelocity"></param>
      /// <param name="radius"></param>
      public static LinearVelocity From(AngularVelocity angularVelocity, Length radius)
        => new(angularVelocity.Value * radius.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(LinearVelocity v) => v.m_value;
      public static explicit operator LinearVelocity(double v) => new(v);

      public static bool operator <(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) < 0;
      public static bool operator <=(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) <= 0;
      public static bool operator >(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) > 0;
      public static bool operator >=(LinearVelocity a, LinearVelocity b) => a.CompareTo(b) >= 0;

      public static LinearVelocity operator -(LinearVelocity v) => new(-v.m_value);
      public static LinearVelocity operator +(LinearVelocity a, double b) => new(a.m_value + b);
      public static LinearVelocity operator +(LinearVelocity a, LinearVelocity b) => a + b.m_value;
      public static LinearVelocity operator /(LinearVelocity a, double b) => new(a.m_value / b);
      public static LinearVelocity operator /(LinearVelocity a, LinearVelocity b) => a / b.m_value;
      public static LinearVelocity operator *(LinearVelocity a, double b) => new(a.m_value * b);
      public static LinearVelocity operator *(LinearVelocity a, LinearVelocity b) => a * b.m_value;
      public static LinearVelocity operator %(LinearVelocity a, double b) => new(a.m_value % b);
      public static LinearVelocity operator %(LinearVelocity a, LinearVelocity b) => a % b.m_value;
      public static LinearVelocity operator -(LinearVelocity a, double b) => new(a.m_value - b);
      public static LinearVelocity operator -(LinearVelocity a, LinearVelocity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LinearVelocity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LinearVelocity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(LinearVelocityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(LinearVelocityUnit unit = DefaultUnit)
        => unit switch
        {
          LinearVelocityUnit.FootPerSecond => m_value * (1250.0 / 381.0),
          LinearVelocityUnit.KilometerPerHour => m_value * (18.0 / 5.0),
          LinearVelocityUnit.Knot => m_value * (3600.0 / 1852.0),
          LinearVelocityUnit.MeterPerSecond => m_value,
          LinearVelocityUnit.MilePerHour => m_value * (3125.0 / 1397.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
