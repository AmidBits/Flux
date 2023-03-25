namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.LuminousIntensityUnit unit, bool preferUnicode, bool useFullName = false)
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

    /// <summary>Luminous intensity. SI unit of candela. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Luminous_intensity"/>
    public readonly record struct LuminousIntensity
      : System.IComparable, System.IComparable<LuminousIntensity>, System.IFormattable, IUnitQuantifiable<double, LuminousIntensityUnit>
    {
      public static readonly LuminousIntensity Zero;

      public const LuminousIntensityUnit DefaultUnit = LuminousIntensityUnit.Candela;

      private readonly double m_value;

      public LuminousIntensity(double value, LuminousIntensityUnit unit = DefaultUnit)
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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(LuminousIntensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(LuminousIntensityUnit unit = DefaultUnit)
        => unit switch
        {
          LuminousIntensityUnit.Candela => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
