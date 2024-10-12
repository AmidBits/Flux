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
    : System.IComparable, System.IComparable<SolidAngle>, System.IFormattable, ISiUnitValueQuantifiable<double, SolidAngleUnit>
  {
    private readonly double m_value;

    public SolidAngle(double value, SolidAngleUnit unit = SolidAngleUnit.Steradian) => m_value = ConvertFromUnit(unit, value);

    public SolidAngle(MetricPrefix prefix, double steradian) => m_value = prefix.ConvertTo(steradian, MetricPrefix.Unprefixed);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SolidAngleUnit.Steradian, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(SolidAngleUnit.Steradian, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(SolidAngleUnit.Steradian, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(SolidAngleUnit unit, double value)
      => unit switch
      {
        SolidAngleUnit.Steradian => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(SolidAngleUnit unit, double value)
      => unit switch
      {
        SolidAngleUnit.Steradian => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, SolidAngleUnit from, SolidAngleUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(SolidAngleUnit unit)
      => unit switch
      {
        SolidAngleUnit.Steradian => 1,
        SolidAngleUnit.Spat => System.Math.Tau + System.Math.Tau,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(SolidAngleUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(SolidAngleUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.SolidAngleUnit.Steradian => preferUnicode ? "\u33DB" : "sr",
        Quantities.SolidAngleUnit.Spat => "sp",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SolidAngleUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SolidAngleUnit unit = SolidAngleUnit.Steradian, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="SolidAngle.Value"/> property is in <see cref="SolidAngleUnit.Steradian"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
