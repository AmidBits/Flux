namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AngularVelocityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AngularVelocityUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AngularVelocityUnit
    {
      RadianPerSecond,
    }

    /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_velocity"/>
    public readonly record struct AngularVelocity
      : System.IComparable, System.IComparable<AngularVelocity>, System.IFormattable, IUnitValueQuantifiable<double, AngularVelocityUnit>
    {
      public const AngularVelocityUnit DefaultUnit = AngularVelocityUnit.RadianPerSecond;

      private readonly double m_value;

      public AngularVelocity(double value, AngularVelocityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AngularVelocityUnit.RadianPerSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Frequency ToFrequency()
        => new(m_value / System.Math.Tau);

      #region Static methods
      /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
      public static double ConvertAngularVelocityToRotationalSpeed(double radPerSecond)
        => radPerSecond / System.Math.Tau;

      /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
      public static double ConvertRotationalSpeedToAngularVelocity(double revolutionPerMinute)
        => revolutionPerMinute / 60;

      public static AngularVelocity From(Angle angle, Time time)
        => new(angle.Value / time.Value);

      /// <summary>Creates a new <see cref="AngularVelocity"/> instance from <see cref="LinearVelocity">tangential/linear speed</see> and <see cref="Length">radius</see></summary>
      /// <param name="tangentialSpeed"></param>
      /// <param name="radius"></param>
      public static AngularVelocity From(LinearVelocity tangentialSpeed, Length radius)
        => new(tangentialSpeed.Value / radius.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(AngularVelocity v) => v.m_value;
      public static explicit operator AngularVelocity(double v) => new(v);

      public static bool operator <(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) < 0;
      public static bool operator <=(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) <= 0;
      public static bool operator >(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) > 0;
      public static bool operator >=(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) >= 0;

      public static AngularVelocity operator -(AngularVelocity v) => new(-v.m_value);
      public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b) => new(a.m_value + b.m_value);
      public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b) => new(a.m_value / b.m_value);
      public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b) => new(a.m_value * b.m_value);
      public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b) => new(a.m_value % b.m_value);
      public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b) => new(a.m_value - b.m_value);

      public static AngularVelocity operator +(AngularVelocity a, double b) => new(a.m_value + b);
      public static AngularVelocity operator /(AngularVelocity a, double b) => new(a.m_value / b);
      public static AngularVelocity operator *(AngularVelocity a, double b) => new(a.m_value * b);
      public static AngularVelocity operator %(AngularVelocity a, double b) => new(a.m_value % b);
      public static AngularVelocity operator -(AngularVelocity a, double b) => new(a.m_value - b);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AngularVelocity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AngularVelocity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AngularVelocityUnit unit)
        => unit switch
        {
          AngularVelocityUnit.RadianPerSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularVelocityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
