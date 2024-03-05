namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AccelerationUnit unit, bool preferUnicode = true, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AccelerationUnit.MeterPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AccelerationUnit
    {
      /// <summary>This is the default unit for <see cref="Acceleration"/>.</summary>
      MeterPerSecondSquared,
    }

    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration
      : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, IUnitValueQuantifiable<double, AccelerationUnit>
    {
      public static Acceleration StandardGravity => new(9.80665);

      private readonly double m_value;

      public Acceleration(double value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(Acceleration a, Acceleration b) => a.CompareTo(b) < 0;
      public static bool operator <=(Acceleration a, Acceleration b) => a.CompareTo(b) <= 0;
      public static bool operator >(Acceleration a, Acceleration b) => a.CompareTo(b) > 0;
      public static bool operator >=(Acceleration a, Acceleration b) => a.CompareTo(b) >= 0;

      public static Acceleration operator -(Acceleration v) => new(-v.m_value);
      public static Acceleration operator +(Acceleration a, double b) => new(a.m_value + b);
      public static Acceleration operator +(Acceleration a, Acceleration b) => a + b.m_value;
      public static Acceleration operator /(Acceleration a, double b) => new(a.m_value / b);
      public static Acceleration operator /(Acceleration a, Acceleration b) => a / b.m_value;
      public static Acceleration operator *(Acceleration a, double b) => new(a.m_value * b);
      public static Acceleration operator *(Acceleration a, Acceleration b) => a * b.m_value;
      public static Acceleration operator %(Acceleration a, double b) => new(a.m_value % b);
      public static Acceleration operator %(Acceleration a, Acceleration b) => a % b.m_value;
      public static Acceleration operator -(Acceleration a, double b) => new(a.m_value - b);
      public static Acceleration operator -(Acceleration a, Acceleration b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Acceleration o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Acceleration other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Acceleration.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
