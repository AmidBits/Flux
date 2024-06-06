namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.IlluminanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.IlluminanceUnit.Lux => preferUnicode ? "\u33D3" : "lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum IlluminanceUnit
    {
      /// <summary>This is the default unit for <see cref="Illuminance"/>.</summary>
      Lux,
    }

    /// <summary>Illuminance unit of lux.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Illuminance"/>
    public readonly record struct Illuminance
      : System.IComparable, System.IComparable<Illuminance>, System.IFormattable, IUnitValueQuantifiable<double, IlluminanceUnit>
    {
      private readonly double m_value;

      public Illuminance(double value, IlluminanceUnit unit = IlluminanceUnit.Lux)
        => m_value = unit switch
        {
          IlluminanceUnit.Lux => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      //public MetricMultiplicative ToMetricMultiplicative() => new(m_value, MetricMultiplicativePrefix.One);

      #region Overloaded operators

      public static bool operator <(Illuminance a, Illuminance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Illuminance a, Illuminance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Illuminance a, Illuminance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Illuminance a, Illuminance b) => a.CompareTo(b) >= 0;

      public static Illuminance operator -(Illuminance v) => new(-v.m_value);
      public static Illuminance operator +(Illuminance a, double b) => new(a.m_value + b);
      public static Illuminance operator +(Illuminance a, Illuminance b) => a + b.m_value;
      public static Illuminance operator /(Illuminance a, double b) => new(a.m_value / b);
      public static Illuminance operator /(Illuminance a, Illuminance b) => a / b.m_value;
      public static Illuminance operator *(Illuminance a, double b) => new(a.m_value * b);
      public static Illuminance operator *(Illuminance a, Illuminance b) => a * b.m_value;
      public static Illuminance operator %(Illuminance a, double b) => new(a.m_value % b);
      public static Illuminance operator %(Illuminance a, Illuminance b) => a % b.m_value;
      public static Illuminance operator -(Illuminance a, double b) => new(a.m_value - b);
      public static Illuminance operator -(Illuminance a, Illuminance b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Illuminance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Illuminance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(IlluminanceUnit.Lux, format, formatProvider);

      // IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(IlluminanceUnit.Lux.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(IlluminanceUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(IlluminanceUnit unit)
        => unit switch
        {
          IlluminanceUnit.Lux => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
