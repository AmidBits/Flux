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

    public Inductance(MetricPrefix prefix, double henry) => m_value = prefix.ChangePrefix(henry, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + InductanceUnit.Henry.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(InductanceUnit unit, double value)
      => unit switch
      {
        InductanceUnit.Henry => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(InductanceUnit unit, double value)
      => unit switch
      {
        InductanceUnit.Henry => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, InductanceUnit from, InductanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(InductanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(InductanceUnit unit = InductanceUnit.Henry, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));
    }

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
