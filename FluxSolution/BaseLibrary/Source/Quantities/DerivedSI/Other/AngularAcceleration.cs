namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.AngularAccelerationUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33AF" : "rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AngularAccelerationUnit
    {
      RadianPerSecondSquared,
    }

    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration
      : System.IComparable, System.IComparable<AngularAcceleration>, System.IFormattable, IUnitQuantifiable<double, AngularAccelerationUnit>
    {
      public static readonly AngularAcceleration Zero;

      public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquared;

      private readonly double m_value;

      public AngularAcceleration(double value, AngularAccelerationUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(AngularAcceleration v) => v.m_value;
      public static explicit operator AngularAcceleration(double v) => new(v);

      public static bool operator <(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) < 0;
      public static bool operator <=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) <= 0;
      public static bool operator >(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) > 0;
      public static bool operator >=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) >= 0;

      public static AngularAcceleration operator -(AngularAcceleration v) => new(-v.m_value);
      public static AngularAcceleration operator +(AngularAcceleration a, double b) => new(a.m_value + b);
      public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b) => a + b.m_value;
      public static AngularAcceleration operator /(AngularAcceleration a, double b) => new(a.m_value / b);
      public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b) => a / b.m_value;
      public static AngularAcceleration operator *(AngularAcceleration a, double b) => new(a.m_value * b);
      public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b) => a * b.m_value;
      public static AngularAcceleration operator %(AngularAcceleration a, double b) => new(a.m_value % b);
      public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b) => a % b.m_value;
      public static AngularAcceleration operator -(AngularAcceleration a, double b) => new(a.m_value - b);
      public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AngularAcceleration o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AngularAcceleration other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AngularAccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
