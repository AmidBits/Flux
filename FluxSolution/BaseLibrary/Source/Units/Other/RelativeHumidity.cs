namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.RelativeHumidityUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.RelativeHumidityUnit.Percent => options.PreferUnicode ? "\u0025" : "\u0025",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum RelativeHumidityUnit
    {
      /// <summary>This is the default unit for <see cref="RelativeHumidity"/>.</summary>
      Percent,
    }

    /// <summary>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
    public readonly record struct RelativeHumidity
      : System.IComparable, System.IComparable<RelativeHumidity>, System.IFormattable, IUnitValueQuantifiable<double, RelativeHumidityUnit>
    {
      public const RelativeHumidityUnit DefaultUnit = RelativeHumidityUnit.Percent;

      private readonly double m_value;

      public RelativeHumidity(double value, RelativeHumidityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          RelativeHumidityUnit.Percent => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(RelativeHumidity v) => v.m_value;
      public static explicit operator RelativeHumidity(double v) => new(v);

      public static bool operator <(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) < 0;
      public static bool operator <=(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) <= 0;
      public static bool operator >(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) > 0;
      public static bool operator >=(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) >= 0;

      public static RelativeHumidity operator -(RelativeHumidity v) => new(-v.m_value);
      public static RelativeHumidity operator +(RelativeHumidity a, double b) => new(a.m_value + b);
      public static RelativeHumidity operator +(RelativeHumidity a, RelativeHumidity b) => a + b.m_value;
      public static RelativeHumidity operator /(RelativeHumidity a, double b) => new(a.m_value / b);
      public static RelativeHumidity operator /(RelativeHumidity a, RelativeHumidity b) => a / b.m_value;
      public static RelativeHumidity operator *(RelativeHumidity a, double b) => new(a.m_value * b);
      public static RelativeHumidity operator *(RelativeHumidity a, RelativeHumidity b) => a * b.m_value;
      public static RelativeHumidity operator %(RelativeHumidity a, double b) => new(a.m_value % b);
      public static RelativeHumidity operator %(RelativeHumidity a, RelativeHumidity b) => a % b.m_value;
      public static RelativeHumidity operator -(RelativeHumidity a, double b) => new(a.m_value - b);
      public static RelativeHumidity operator -(RelativeHumidity a, RelativeHumidity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is RelativeHumidity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(RelativeHumidity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="RelativeHumidity.Value"/> property is in <see cref="RelativeHumidityUnit.Percent"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(RelativeHumidityUnit unit)
        => unit switch
        {
          RelativeHumidityUnit.Percent => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(RelativeHumidityUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
