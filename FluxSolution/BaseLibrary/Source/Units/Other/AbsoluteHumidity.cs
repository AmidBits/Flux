namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AbsoluteHumidityUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.AbsoluteHumidityUnit.GramsPerCubicMeter => "g/m³",
        Units.AbsoluteHumidityUnit.KilogramsPerCubicMeter => "kg/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AbsoluteHumidityUnit
    {
      /// <summary>This is the default unit for <see cref="AbsoluteHumidity"/>.</summary>
      GramsPerCubicMeter,
      KilogramsPerCubicMeter,
    }

    /// <summary>Absolute humidity unit of grams per cubic meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
    public readonly record struct AbsoluteHumidity
      : System.IComparable, System.IComparable<AbsoluteHumidity>, System.IFormattable, IUnitValueQuantifiable<double, AbsoluteHumidityUnit>
    {
      public const AbsoluteHumidityUnit DefaultUnit = AbsoluteHumidityUnit.GramsPerCubicMeter;

      private readonly double m_value;

      public AbsoluteHumidity(double value, AbsoluteHumidityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AbsoluteHumidityUnit.GramsPerCubicMeter => value,
          AbsoluteHumidityUnit.KilogramsPerCubicMeter => value / 1000.0,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      public static AbsoluteHumidity From(double grams, Volume volume)
        => new(grams / volume.Value);
      public static AbsoluteHumidity From(Mass mass, Volume volume)
        => From(mass.Value * 1000, volume);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(AbsoluteHumidity v) => v.m_value;
      public static explicit operator AbsoluteHumidity(double v) => new(v);

      public static bool operator <(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) < 0;
      public static bool operator <=(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) <= 0;
      public static bool operator >(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) > 0;
      public static bool operator >=(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) >= 0;

      public static AbsoluteHumidity operator -(AbsoluteHumidity v) => new(-v.m_value);
      public static AbsoluteHumidity operator +(AbsoluteHumidity a, double b) => new(a.m_value + b);
      public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b) => a + b.m_value;
      public static AbsoluteHumidity operator /(AbsoluteHumidity a, double b) => new(a.m_value / b);
      public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b) => a / b.m_value;
      public static AbsoluteHumidity operator *(AbsoluteHumidity a, double b) => new(a.m_value * b);
      public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b) => a * b.m_value;
      public static AbsoluteHumidity operator %(AbsoluteHumidity a, double b) => new(a.m_value % b);
      public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b) => a % b.m_value;
      public static AbsoluteHumidity operator -(AbsoluteHumidity a, double b) => new(a.m_value - b);
      public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AbsoluteHumidity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AbsoluteHumidity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="AbsoluteHumidity.Value"/> property is in <see cref="AbsoluteHumidityUnit.GramsPerCubicMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AbsoluteHumidityUnit unit)
        => unit switch
        {
          AbsoluteHumidityUnit.GramsPerCubicMeter => m_value,
          AbsoluteHumidityUnit.KilogramsPerCubicMeter => m_value * 1000.0,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AbsoluteHumidityUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
