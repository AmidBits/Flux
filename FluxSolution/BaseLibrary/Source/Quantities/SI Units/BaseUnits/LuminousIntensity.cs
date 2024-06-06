namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.LuminousIntensityUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum LuminousIntensityUnit
    {
      Candela,
    }

    /// <summary>
    /// <para>Luminous intensity. SI unit of candela. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Luminous_intensity"/></para>
    /// </summary>
    public readonly record struct LuminousIntensity
      : System.IComparable, System.IComparable<LuminousIntensity>, System.IFormattable, ISiPrefixValueQuantifiable<double, LuminousIntensityUnit>
    {
      private readonly double m_value;

      public LuminousIntensity(double value, LuminousIntensityUnit unit = LuminousIntensityUnit.Candela)
        => m_value = unit switch
        {
          LuminousIntensityUnit.Candela => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Mega"/> for megacandelas.</para>
      /// </summary>
      /// <param name="candelas"></param>
      /// <param name="prefix"></param>
      public LuminousIntensity(double candelas, MetricPrefix prefix) => m_value = prefix.Convert(candelas, MetricPrefix.NoPrefix);

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

      // ISiUnitValueQuantifiable<>
      public LuminousIntensityUnit BaseUnit => LuminousIntensityUnit.Candela;

      public LuminousIntensityUnit UnprefixedUnit => LuminousIntensityUnit.Candela;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetSiPrefixSymbol(prefix, preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="LuminousIntensity.Value"/> property is in <see cref="LuminousIntensityUnit.Candela"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(LuminousIntensityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(LuminousIntensityUnit unit)
        => unit switch
        {
          LuminousIntensityUnit.Candela => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
