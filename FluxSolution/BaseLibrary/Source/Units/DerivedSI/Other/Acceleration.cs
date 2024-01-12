namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AccelerationUnit unit, bool preferUnicode, bool useFullName = false)
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
      MeterPerSecondSquared,
    }

    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration
      : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, IUnitValueQuantifiable<double, AccelerationUnit>
    {
      public const AccelerationUnit DefaultUnit = AccelerationUnit.MeterPerSecondSquared;

      public static Acceleration StandardGravity => new(9.80665);

      private readonly double m_value;

      public Acceleration(double value, AccelerationUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Acceleration v) => v.m_value;
      public static explicit operator Acceleration(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => ToValueString(format);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
