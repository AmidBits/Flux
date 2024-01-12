namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.RelativeHumidityUnit unit, bool preferUnicode, bool useFullName)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.RelativeHumidityUnit.Percent => preferUnicode ? "\u0025" : "\u0025",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum RelativeHumidityUnit
    {
      Percent,
    }

    /// <summary>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
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
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(RelativeHumidityUnit unit)
        => unit switch
        {
          RelativeHumidityUnit.Percent => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(RelativeHumidityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
