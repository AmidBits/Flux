namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LuminousIntensityUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(LuminousIntensityUnit.Candela, format, formatProvider);

      //IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.Count.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.None)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(LengthUnit.Metre.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
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

      public string ToUnitValueString(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
