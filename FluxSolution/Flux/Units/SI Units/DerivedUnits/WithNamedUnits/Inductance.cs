namespace Flux.Units
{
  /// <summary>
  /// <para>Electrical inductance, unit of Henry.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Inductance"/></para>
  /// </summary>
  public readonly record struct Inductance
    : System.IComparable, System.IComparable<Inductance>, System.IFormattable, ISiUnitValueQuantifiable<double, InductanceUnit>
  {
    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = InductanceUnit.Henry) => m_value = ConvertFromUnit(unit, value);

    public Inductance(MetricPrefix prefix, double henry) => m_value = prefix.ConvertTo(henry, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Inductance a, Inductance b) => a.CompareTo(b) < 0;
    public static bool operator >(Inductance a, Inductance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Inductance a, Inductance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Inductance a, Inductance b) => a.CompareTo(b) >= 0;

    public static Inductance operator -(Inductance v) => new(-v.m_value);
    public static Inductance operator *(Inductance a, Inductance b) => new(a.m_value * b.m_value);
    public static Inductance operator /(Inductance a, Inductance b) => new(a.m_value / b.m_value);
    public static Inductance operator %(Inductance a, Inductance b) => new(a.m_value % b.m_value);
    public static Inductance operator +(Inductance a, Inductance b) => new(a.m_value + b.m_value);
    public static Inductance operator -(Inductance a, Inductance b) => new(a.m_value - b.m_value);
    public static Inductance operator *(Inductance a, double b) => new(a.m_value * b);
    public static Inductance operator /(Inductance a, double b) => new(a.m_value / b);
    public static Inductance operator %(Inductance a, double b) => new(a.m_value % b);
    public static Inductance operator +(Inductance a, double b) => new(a.m_value + b);
    public static Inductance operator -(Inductance a, double b) => new(a.m_value - b);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Inductance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Inductance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(InductanceUnit.Henry, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(InductanceUnit.Henry, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(InductanceUnit unit, double value)
      => unit switch
      {
        InductanceUnit.Henry => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(InductanceUnit unit, double value)
      => unit switch
      {
        InductanceUnit.Henry => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, InductanceUnit from, InductanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(InductanceUnit unit)
      => unit switch
      {
        InductanceUnit.Henry => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(InductanceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(InductanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(InductanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(InductanceUnit unit = InductanceUnit.Henry, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Inductance.Value"/> property is in <see cref="InductanceUnit.Henry"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
