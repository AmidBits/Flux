namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AngularFrequencyUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.AngularFrequencyUnit.RadianPerSecond => options.PreferUnicode ? "\u33AE" : "rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AngularFrequencyUnit
    {
      /// <summary>This is the default unit for <see cref="AngularFrequency"/>.</summary>
      RadianPerSecond,
    }

    /// <summary>Angular frequency (also called angular speed and angular rate), unit of radians per second. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_frequency"/>
    public readonly record struct AngularFrequency
      : System.IComparable, System.IComparable<AngularFrequency>, System.IFormattable, IUnitValueQuantifiable<double, AngularFrequencyUnit>
    {
      public const AngularFrequencyUnit DefaultUnit = AngularFrequencyUnit.RadianPerSecond;

      private readonly double m_value;

      public AngularFrequency(double value, AngularFrequencyUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AngularFrequencyUnit.RadianPerSecond => value,
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

      public static AngularFrequency From(Angle angle, Time time)
        => new(angle.Value / time.Value);

      /// <summary>Creates a new <see cref="AngularFrequency"/> instance from <see cref="Speed">tangential/linear speed</see> and <see cref="Length">radius</see></summary>
      /// <param name="tangentialSpeed"></param>
      /// <param name="radius"></param>
      public static AngularFrequency From(Speed tangentialSpeed, Length radius)
        => new(tangentialSpeed.Value / radius.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(AngularFrequency v) => v.m_value;
      public static explicit operator AngularFrequency(double v) => new(v);

      public static bool operator <(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) < 0;
      public static bool operator <=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) <= 0;
      public static bool operator >(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) > 0;
      public static bool operator >=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) >= 0;

      public static AngularFrequency operator -(AngularFrequency v) => new(-v.m_value);
      public static AngularFrequency operator +(AngularFrequency a, AngularFrequency b) => new(a.m_value + b.m_value);
      public static AngularFrequency operator /(AngularFrequency a, AngularFrequency b) => new(a.m_value / b.m_value);
      public static AngularFrequency operator *(AngularFrequency a, AngularFrequency b) => new(a.m_value * b.m_value);
      public static AngularFrequency operator %(AngularFrequency a, AngularFrequency b) => new(a.m_value % b.m_value);
      public static AngularFrequency operator -(AngularFrequency a, AngularFrequency b) => new(a.m_value - b.m_value);

      public static AngularFrequency operator +(AngularFrequency a, double b) => new(a.m_value + b);
      public static AngularFrequency operator /(AngularFrequency a, double b) => new(a.m_value / b);
      public static AngularFrequency operator *(AngularFrequency a, double b) => new(a.m_value * b);
      public static AngularFrequency operator %(AngularFrequency a, double b) => new(a.m_value % b);
      public static AngularFrequency operator -(AngularFrequency a, double b) => new(a.m_value - b);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AngularFrequency o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AngularFrequency other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="AngularFrequency.Value"/> property is in <see cref="AngularFrequencyUnit.RadianPerSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AngularFrequencyUnit unit)
        => unit switch
        {
          AngularFrequencyUnit.RadianPerSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularFrequencyUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
