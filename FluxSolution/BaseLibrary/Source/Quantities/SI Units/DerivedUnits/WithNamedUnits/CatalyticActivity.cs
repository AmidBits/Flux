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

    #region Overloaded operators

    public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
    public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
    public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
    public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

    public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
    public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
    public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => a + b.m_value;
    public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
    public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => a / b.m_value;
    public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
    public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => a * b.m_value;
    public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
    public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => a % b.m_value;
    public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);
    public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(CatalyticActivityUnit.Katal, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(CatalyticActivityUnit.Katal, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="CatalyticActivity.Value"/> property is in <see cref="CatalyticActivityUnit.Katal"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

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

    public string GetUnitName(CatalyticActivityUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(CatalyticActivityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CatalyticActivityUnit.Katal => preferUnicode ? "\u33CF" : "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CatalyticActivityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
