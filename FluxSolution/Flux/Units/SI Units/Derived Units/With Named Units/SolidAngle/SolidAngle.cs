namespace Flux.Units
{
  /// <summary>
  /// <para>Solid angle, unit of steradian.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Solid_angle"/></para>
  /// </summary>
  public readonly record struct SolidAngle
    : System.IComparable, System.IComparable<SolidAngle>, System.IFormattable, ISiUnitValueQuantifiable<double, SolidAngleUnit>
  {
    private readonly double m_value;

    public SolidAngle(double value, SolidAngleUnit unit = SolidAngleUnit.Steradian) => m_value = ConvertFromUnit(unit, value);

    public SolidAngle(MetricPrefix prefix, double steradian) => m_value = prefix.ConvertPrefix(steradian, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(SolidAngle a, SolidAngle b) => a.CompareTo(b) < 0;
    public static bool operator >(SolidAngle a, SolidAngle b) => a.CompareTo(b) > 0;
    public static bool operator <=(SolidAngle a, SolidAngle b) => a.CompareTo(b) <= 0;
    public static bool operator >=(SolidAngle a, SolidAngle b) => a.CompareTo(b) >= 0;

    public static SolidAngle operator -(SolidAngle v) => new(-v.m_value);
    public static SolidAngle operator *(SolidAngle a, SolidAngle b) => new(a.m_value * b.m_value);
    public static SolidAngle operator /(SolidAngle a, SolidAngle b) => new(a.m_value / b.m_value);
    public static SolidAngle operator %(SolidAngle a, SolidAngle b) => new(a.m_value % b.m_value);
    public static SolidAngle operator +(SolidAngle a, SolidAngle b) => new(a.m_value + b.m_value);
    public static SolidAngle operator -(SolidAngle a, SolidAngle b) => new(a.m_value - b.m_value);
    public static SolidAngle operator *(SolidAngle a, double b) => new(a.m_value * b);
    public static SolidAngle operator /(SolidAngle a, double b) => new(a.m_value / b);
    public static SolidAngle operator %(SolidAngle a, double b) => new(a.m_value % b);
    public static SolidAngle operator +(SolidAngle a, double b) => new(a.m_value + b);
    public static SolidAngle operator -(SolidAngle a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is SolidAngle o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(SolidAngle other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SolidAngleUnit.Steradian, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + SolidAngleUnit.Steradian.GetUnitName(preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + SolidAngleUnit.Steradian.GetUnitSymbol(preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + SolidAngleUnit.Steradian.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(SolidAngleUnit unit, double value)
      => unit switch
      {
        SolidAngleUnit.Steradian => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(SolidAngleUnit unit, double value)
      => unit switch
      {
        SolidAngleUnit.Steradian => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, SolidAngleUnit from, SolidAngleUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(SolidAngleUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SolidAngleUnit unit = SolidAngleUnit.Steradian, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
