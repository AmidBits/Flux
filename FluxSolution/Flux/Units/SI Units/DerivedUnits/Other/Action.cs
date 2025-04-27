namespace Flux.Units
{
  /// <summary>Action. Unit of Joule second.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Action_(physics)"/>
  public readonly record struct Action
    : System.IComparable, System.IComparable<Action>, System.IFormattable, ISiUnitValueQuantifiable<double, ActionUnit>
  {
    /// <summary>
    /// <para>The Planck constant, or Planck's constant, denoted by h, is a fundamental physical constant of foundational importance in quantum mechanics: a photon's energy is equal to its frequency multiplied by the Planck constant, and the wavelength of a matter wave equals the Planck constant divided by the associated particle momentum.</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Planck_constant"/></para>
    /// </summary>
    public const double PlanckConstant = 6.62607015e-34;

    private readonly double m_value;

    public Action(double value, ActionUnit unit = ActionUnit.JouleSecond) => m_value = ConvertFromUnit(unit, value);

    public Action(MetricPrefix prefix, double jouleSecond) => m_value = prefix.ConvertTo(jouleSecond, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Action a, Action b) => a.CompareTo(b) < 0;
    public static bool operator >(Action a, Action b) => a.CompareTo(b) > 0;
    public static bool operator <=(Action a, Action b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Action a, Action b) => a.CompareTo(b) >= 0;

    public static Action operator -(Action v) => new(-v.m_value);
    public static Action operator *(Action a, Action b) => new(a.m_value * b.m_value);
    public static Action operator /(Action a, Action b) => new(a.m_value / b.m_value);
    public static Action operator %(Action a, Action b) => new(a.m_value % b.m_value);
    public static Action operator +(Action a, Action b) => new(a.m_value + b.m_value);
    public static Action operator -(Action a, Action b) => new(a.m_value - b.m_value);
    public static Action operator *(Action a, double b) => new(a.m_value * b);
    public static Action operator /(Action a, double b) => new(a.m_value / b);
    public static Action operator %(Action a, double b) => new(a.m_value % b);
    public static Action operator +(Action a, double b) => new(a.m_value + b);
    public static Action operator -(Action a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Action o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Action other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ActionUnit.JouleSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ActionUnit.JouleSecond, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ActionUnit.JouleSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ActionUnit unit, double value)
      => unit switch
      {
        ActionUnit.JouleSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ActionUnit unit, double value)
      => unit switch
      {
        ActionUnit.JouleSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ActionUnit from, ActionUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ActionUnit unit)
      => unit switch
      {
        ActionUnit.JouleSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ActionUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ActionUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.ActionUnit.JouleSecond => preferUnicode ? "J\u22C5s" : "J·s",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ActionUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ActionUnit unit = ActionUnit.JouleSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Action.Value"/> property is in <see cref="ActionUnit.JouleSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
