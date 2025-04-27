namespace Flux.Units
{
  /// <summary>
  /// <para>Impulse, unit of Newton second.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Impulse"/></para>
  /// </summary>
  public readonly record struct Impulse
    : System.IComparable, System.IComparable<Impulse>, System.IFormattable, ISiUnitValueQuantifiable<double, ImpulseUnit>
  {
    private readonly double m_value;

    public Impulse(double value, ImpulseUnit unit = ImpulseUnit.NewtonSecond) => m_value = ConvertToUnit(unit, value);

    public Impulse(MetricPrefix prefix, double newtonSecond) => m_value = prefix.ConvertTo(newtonSecond, MetricPrefix.Unprefixed);

    public Impulse(Force force, Time time) : this(force.Value / time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
    public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
    public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

    public static Impulse operator -(Impulse v) => new(-v.m_value);
    public static Impulse operator *(Impulse a, Impulse b) => new(a.m_value * b.m_value);
    public static Impulse operator /(Impulse a, Impulse b) => new(a.m_value / b.m_value);
    public static Impulse operator %(Impulse a, Impulse b) => new(a.m_value % b.m_value);
    public static Impulse operator +(Impulse a, Impulse b) => new(a.m_value + b.m_value);
    public static Impulse operator -(Impulse a, Impulse b) => new(a.m_value - b.m_value);
    public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
    public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
    public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
    public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
    public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ImpulseUnit.NewtonSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ImpulseUnit.NewtonSecond, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ImpulseUnit.NewtonSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ImpulseUnit from, ImpulseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ImpulseUnit unit)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ImpulseUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ImpulseUnit unit, bool preferUnicode)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N·s",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ImpulseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ImpulseUnit unit = ImpulseUnit.NewtonSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Impulse.Value"/> property is in <see cref="ImpulseUnit.NewtonSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
