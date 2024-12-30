namespace Flux.Quantities
{
  public enum CatalyticActivityUnit
  {
    /// <summary>This is the default unit for <see cref="CatalyticActivity"/>. Katal = (mol/s).</summary>
    Katal,
  }

  /// <summary>Catalytic activity unit of Katal.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Catalysis"/>
  public readonly record struct CatalyticActivity
    : System.IComparable, System.IComparable<CatalyticActivity>, System.IFormattable, ISiUnitValueQuantifiable<double, CatalyticActivityUnit>
  {
    private readonly double m_value;

    public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal) => m_value = ConvertFromUnit(unit, value);

    public CatalyticActivity(MetricPrefix prefix, double katal) => m_value = prefix.ConvertTo(katal, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
    public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
    public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

    public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
    public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => new(a.m_value * b.m_value);
    public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => new(a.m_value / b.m_value);
    public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => new(a.m_value % b.m_value);
    public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => new(a.m_value + b.m_value);
    public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => new(a.m_value - b.m_value);
    public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
    public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
    public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
    public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
    public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(CatalyticActivityUnit.Katal, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(CatalyticActivityUnit.Katal, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(CatalyticActivityUnit unit, double value)
      => unit switch
      {
        CatalyticActivityUnit.Katal => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(CatalyticActivityUnit unit, double value)
      => unit switch
      {
        CatalyticActivityUnit.Katal => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, CatalyticActivityUnit from, CatalyticActivityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(CatalyticActivityUnit unit)
      => unit switch
      {
        CatalyticActivityUnit.Katal => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(CatalyticActivityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(CatalyticActivityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CatalyticActivityUnit.Katal => preferUnicode ? "\u33CF" : "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CatalyticActivityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="CatalyticActivity.Value"/> property is in <see cref="CatalyticActivityUnit.Katal"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
