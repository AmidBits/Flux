namespace Flux.Units
{
  /// <summary>
  /// <para>Force, unit of newton. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Force"/></para>
  /// </summary>
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

    public Force(MetricPrefix prefix, double newton) => m_value = prefix.ChangePrefix(newton, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ForceUnit.Newton.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ForceUnit unit, double value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ForceUnit from, ForceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ForceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ForceUnit unit = ForceUnit.Newton, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
