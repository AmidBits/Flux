namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SolidAngleUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.SolidAngleUnit.Steradian => preferUnicode ? "\u33DB" : "sr",
        Units.SolidAngleUnit.Spat => "sp",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum SolidAngleUnit
    {
      /// <summary>Steradian.</summary>
      Steradian,
      Spat,
    }

    /// <summary>Solid angle. Unit of steradian.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Solid_angle"/>
    public readonly record struct SolidAngle
      : System.IComparable, System.IComparable<SolidAngle>, System.IFormattable, IUnitValueQuantifiable<double, SolidAngleUnit>
    {
      public const SolidAngleUnit DefaultUnit = SolidAngleUnit.Steradian;

      private readonly double m_value;

      public SolidAngle(double value, SolidAngleUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          SolidAngleUnit.Spat => value / (System.Math.Tau + System.Math.Tau),
          SolidAngleUnit.Steradian => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(SolidAngle v) => v.m_value;
      public static explicit operator SolidAngle(double v) => new(v);

      public static bool operator <(SolidAngle a, SolidAngle b) => a.CompareTo(b) < 0;
      public static bool operator <=(SolidAngle a, SolidAngle b) => a.CompareTo(b) <= 0;
      public static bool operator >(SolidAngle a, SolidAngle b) => a.CompareTo(b) > 0;
      public static bool operator >=(SolidAngle a, SolidAngle b) => a.CompareTo(b) >= 0;

      public static SolidAngle operator -(SolidAngle v) => new(-v.m_value);
      public static SolidAngle operator +(SolidAngle a, double b) => new(a.m_value + b);
      public static SolidAngle operator +(SolidAngle a, SolidAngle b) => a + b.m_value;
      public static SolidAngle operator /(SolidAngle a, double b) => new(a.m_value / b);
      public static SolidAngle operator /(SolidAngle a, SolidAngle b) => a / b.m_value;
      public static SolidAngle operator *(SolidAngle a, double b) => new(a.m_value * b);
      public static SolidAngle operator *(SolidAngle a, SolidAngle b) => a * b.m_value;
      public static SolidAngle operator %(SolidAngle a, double b) => new(a.m_value % b);
      public static SolidAngle operator %(SolidAngle a, SolidAngle b) => a % b.m_value;
      public static SolidAngle operator -(SolidAngle a, double b) => new(a.m_value - b);
      public static SolidAngle operator -(SolidAngle a, SolidAngle b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is SolidAngle o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(SolidAngle other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(SolidAngleUnit unit)
        => unit switch
        {
          SolidAngleUnit.Spat => m_value * (System.Math.Tau + System.Math.Tau),
          SolidAngleUnit.Steradian => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SolidAngleUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
