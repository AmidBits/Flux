namespace Flux.Quantities
{
  public enum ForceUnit
  {
    /// <summary>This is the default unit for <see cref="Force"/>.</summary>
    Newton,
    KilogramForce,
    PoundForce,
  }

  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Force"/>
  public readonly record struct Force
    : System.IComparable, System.IComparable<Force>, System.IFormattable, ISiUnitValueQuantifiable<double, ForceUnit>
  {
    /// <summary>
    /// <para>The gravitational constant is an empirical physical constant involved in the calculation of gravitational effects in Sir Isaac Newton's law of universal gravitation and in Albert Einstein's theory of general relativity. It is also known as the universal gravitational constant, the Newtonian constant of gravitation, or the Cavendish gravitational constant, denoted by the capital letter G.</para>
    /// <para>This is fundamental constant of nature. It is not an exact constant.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Gravitational_constant"/></para>
    /// </summary>
    public const double GravitationalConstant = 6.6743e-11;

    private readonly double m_value;

    public Force(double value, ForceUnit unit = ForceUnit.Newton) => m_value = ConvertFromUnit(unit, value);

    public Force(MetricPrefix prefix, double newton) => m_value = prefix.ConvertTo(newton, MetricPrefix.Unprefixed);

    public Force(Mass mass, Acceleration acceleration) => m_value = mass.Value * acceleration.Value;

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Force a, Force b) => a.CompareTo(b) < 0;
    public static bool operator >(Force a, Force b) => a.CompareTo(b) > 0;
    public static bool operator <=(Force a, Force b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Force a, Force b) => a.CompareTo(b) >= 0;

    public static Force operator -(Force v) => new(-v.m_value);
    public static Force operator *(Force a, Force b) => new(a.m_value * b.m_value);
    public static Force operator /(Force a, Force b) => new(a.m_value / b.m_value);
    public static Force operator %(Force a, Force b) => new(a.m_value % b.m_value);
    public static Force operator +(Force a, Force b) => new(a.m_value + b.m_value);
    public static Force operator -(Force a, Force b) => new(a.m_value - b.m_value);
    public static Force operator *(Force a, double b) => new(a.m_value * b);
    public static Force operator /(Force a, double b) => new(a.m_value / b);
    public static Force operator %(Force a, double b) => new(a.m_value % b);
    public static Force operator +(Force a, double b) => new(a.m_value + b);
    public static Force operator -(Force a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Force o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Force other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ForceUnit.Newton, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ForceUnit.Newton, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ForceUnit from, ForceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ForceUnit unit)
      => unit switch
      {
        ForceUnit.Newton => 1,
        ForceUnit.KilogramForce => 0.101971621,
        ForceUnit.PoundForce => 0.224808943,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ForceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ForceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ForceUnit.Newton => "N",
        Quantities.ForceUnit.KilogramForce => "kgf",
        Quantities.ForceUnit.PoundForce => "lbf",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ForceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ForceUnit unit = ForceUnit.Newton, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Force.Value"/> property is in <see cref="ForceUnit.Newton"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
