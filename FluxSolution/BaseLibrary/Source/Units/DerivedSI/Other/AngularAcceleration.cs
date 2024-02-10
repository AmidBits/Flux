namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AngularAccelerationUnit unit, Units.TextOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.AngularAccelerationUnit.RadianPerSecondSquared => options.PreferUnicode ? "\u33AF" : "rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AngularAccelerationUnit
    {
      /// <summary>This is the default unit for <see cref="AngularAcceleration"/>.</summary>
      RadianPerSecondSquared,
    }

    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration
      : System.IComparable, System.IComparable<AngularAcceleration>, System.IFormattable, IUnitValueQuantifiable<double, AngularAccelerationUnit>
    {
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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="AngularAcceleration.Value"/> property is in <see cref="AngularAccelerationUnit.RadianPerSecondSquared"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AngularAccelerationUnit unit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularAccelerationUnit unit, TextOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
