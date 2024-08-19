namespace Flux.Quantities
{
  public enum SolidAngleUnit
  {
    /// <summary>This is the default unit for <see cref="SolidAngle"/>.</summary>
    Steradian,
    Spat,
  }

  /// <summary>Solid angle. Unit of steradian.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Solid_angle"/>
  public readonly record struct SolidAngle
    : System.IComparable, System.IComparable<SolidAngle>, System.IFormattable, IUnitValueQuantifiable<double, SolidAngleUnit>
  {
    private readonly double m_value;

    public SolidAngle(double value, SolidAngleUnit unit = SolidAngleUnit.Steradian)
      => m_value = unit switch
      {
        SolidAngleUnit.Spat => value / (System.Math.Tau + System.Math.Tau),
        SolidAngleUnit.Steradian => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(SolidAngleUnit.Steradian, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="SolidAngle.Value"/> property is in <see cref="SolidAngleUnit.Steradian"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(SolidAngleUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(SolidAngleUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.SolidAngleUnit.Steradian => preferUnicode ? "\u33DB" : "sr",
        Quantities.SolidAngleUnit.Spat => "sp",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SolidAngleUnit unit)
      => unit switch
      {
        SolidAngleUnit.Spat => m_value * (System.Math.Tau + System.Math.Tau),
        SolidAngleUnit.Steradian => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(SolidAngleUnit unit = SolidAngleUnit.Steradian, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(SolidAngleUnit unit = SolidAngleUnit.Steradian, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
