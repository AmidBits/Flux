namespace Flux.Units
{
  /// <summary>
  /// <para>Catalytic activity, unit of Katal.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Catalysis"/></para>
  /// </summary>
  public readonly record struct CatalyticActivity
    : System.IComparable, System.IComparable<CatalyticActivity>, System.IFormattable, ISiUnitValueQuantifiable<double, CatalyticActivityUnit>
  {
    private readonly double m_value;

    public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal) => m_value = ConvertFromUnit(unit, value);

    public CatalyticActivity(MetricPrefix prefix, double katal) => m_value = prefix.ChangePrefix(katal, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + CatalyticActivityUnit.Katal.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(CatalyticActivityUnit unit, double value)
      => unit switch
      {
        CatalyticActivityUnit.Katal => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(CatalyticActivityUnit unit, double value)
      => unit switch
      {
        CatalyticActivityUnit.Katal => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, CatalyticActivityUnit from, CatalyticActivityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(CatalyticActivityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
