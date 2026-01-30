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

    public Action(MetricPrefix prefix, double jouleSecond) => m_value = prefix.ConvertPrefix(jouleSecond, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ActionUnit.JouleSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ActionUnit unit, double value)
      => unit switch
      {
        ActionUnit.JouleSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ActionUnit unit, double value)
      => unit switch
      {
        ActionUnit.JouleSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ActionUnit from, ActionUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ActionUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ActionUnit unit = ActionUnit.JouleSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
