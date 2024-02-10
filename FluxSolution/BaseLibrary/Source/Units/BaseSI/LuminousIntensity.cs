namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LuminousIntensityUnit unit, Units.TextOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.LuminousIntensityUnit.Candela => options.PreferUnicode ? "\u33C5" : "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LuminousIntensityUnit
    {
      Candela,
    }

    /// <summary>Luminous intensity. SI unit of candela. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Luminous_intensity"/>
    public readonly record struct LuminousIntensity
      : System.IComparable, System.IComparable<LuminousIntensity>, System.IFormattable, IMetricMultiplicable<double>, IUnitValueQuantifiable<double, LuminousIntensityUnit>
    {
      private readonly double m_value;

      public LuminousIntensity(double value, LuminousIntensityUnit unit = LuminousIntensityUnit.Candela)
        => m_value = unit switch
        {
          LuminousIntensityUnit.Candela => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(LuminousIntensity v) => v.m_value;
      public static explicit operator LuminousIntensity(double v) => new(v);

      public static bool operator <(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) >= 0;

      public static LuminousIntensity operator -(LuminousIntensity v) => new(-v.m_value);
      public static LuminousIntensity operator +(LuminousIntensity a, double b) => new(a.m_value + b);
      public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b) => a + b.m_value;
      public static LuminousIntensity operator /(LuminousIntensity a, double b) => new(a.m_value / b);
      public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b) => a / b.m_value;
      public static LuminousIntensity operator *(LuminousIntensity a, double b) => new(a.m_value * b);
      public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b) => a * b.m_value;
      public static LuminousIntensity operator %(LuminousIntensity a, double b) => new(a.m_value % b);
      public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b) => a % b.m_value;
      public static LuminousIntensity operator -(LuminousIntensity a, double b) => new(a.m_value - b);
      public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LuminousIntensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LuminousIntensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      //IMetricMultiplicable<>
      public double ToMetricValue(MetricPrefix prefix) => MetricPrefix.Count.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnitSpacing spacing = UnitSpacing.NarrowNoBreakSpace)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(ToMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToChar());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(LengthUnit.Meter.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default) => ToUnitValueString(LuminousIntensityUnit.Candela, options);

      /// <summary>
      /// <para>The unit of the <see cref="LuminousIntensity.Value"/> property is in <see cref="LuminousIntensityUnit.Candela"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LuminousIntensityUnit unit)
        => unit switch
        {
          LuminousIntensityUnit.Candela => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousIntensityUnit unit, string? format = null, System.IFormatProvider? formatProvider = null, UnitSpacing spacing = UnitSpacing.NarrowNoBreakSpace)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(spacing.ToChar());
        sb.Append(unit.GetUnitString());
        return sb.ToString();
      }

      public string ToUnitValueString(LuminousIntensityUnit unit, TextOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
