namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.RelativeHumidityUnit unit, bool preferUnicode = false, bool useFullName = false)
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
      /// <summary>This is the default unit for <see cref="RelativeHumidity"/>.</summary>
      Percent,
    }

    /// <summary>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
    public readonly record struct RelativeHumidity
      : System.IComparable, System.IComparable<RelativeHumidity>, System.IFormattable, IUnitValueQuantifiable<double, RelativeHumidityUnit>
    {
      private readonly double m_value;

      public RelativeHumidity(double value, RelativeHumidityUnit unit = RelativeHumidityUnit.Percent)
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
      public string ToString(string? format, IFormatProvider? formatProvider)
        => ToUnitValueString(RelativeHumidityUnit.Percent, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
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

      public string ToUnitValueString(RelativeHumidityUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces
    }
  }
}
