namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AngularFrequencyUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AngularFrequencyUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",
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

    /// <summary>Angular frequency (a.k.a. angular speed, angular rate), unit of radians per second. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_frequency"/>
    public readonly record struct AngularFrequency
      : System.IComparable, System.IComparable<AngularFrequency>, System.IFormattable, IUnitValueQuantifiable<double, AngularFrequencyUnit>
    {
      private readonly double m_value;

      public AngularFrequency(double value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond)
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AngularFrequencyUnit.RadianPerSecond, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
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

      public string ToUnitValueString(AngularFrequencyUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(AngularFrequencyUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
