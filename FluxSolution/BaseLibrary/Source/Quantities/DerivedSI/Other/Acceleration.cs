namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.AccelerationUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AccelerationUnit.MeterPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AccelerationUnit
    {
      MeterPerSecondSquared,
    }

    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration
      : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, IUnitQuantifiable<double, AccelerationUnit>
    {
      public static readonly Acceleration Zero;

      public const AccelerationUnit DefaultUnit = AccelerationUnit.MeterPerSecondSquared;

      public static Acceleration StandardAccelerationOfGravity
        => new(9.80665);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => ToQuantityString(format);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(AccelerationUnit unit = DefaultUnit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
