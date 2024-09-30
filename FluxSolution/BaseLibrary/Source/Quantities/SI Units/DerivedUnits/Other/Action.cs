namespace Flux.Quantities
{
  public enum ActionUnit
  {
    /// <summary>This is the default unit for <see cref="Action"/>.</summary>
    JouleSecond,
  }

  /// <summary>Action. Unit of Joule second.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Action_(physics)"/>
  public readonly record struct Action
    : System.IComparable, System.IComparable<Action>, System.IFormattable, IUnitValueQuantifiable<double, ActionUnit>
  {
    public static readonly Action PlanckConstant = new(6.62607015e-34);

    private readonly double m_value;

    public Action(double value, ActionUnit unit = ActionUnit.JouleSecond) => m_value = ConvertFromUnit(unit, value);

    #region Overloaded operators

    public static bool operator <(Action a, Action b) => a.CompareTo(b) < 0;
    public static bool operator <=(Action a, Action b) => a.CompareTo(b) <= 0;
    public static bool operator >(Action a, Action b) => a.CompareTo(b) > 0;
    public static bool operator >=(Action a, Action b) => a.CompareTo(b) >= 0;

    public static Action operator -(Action v) => new(-v.m_value);
    public static Action operator +(Action a, double b) => new(a.m_value + b);
    public static Action operator +(Action a, Action b) => a + b.m_value;
    public static Action operator /(Action a, double b) => new(a.m_value / b);
    public static Action operator /(Action a, Action b) => a / b.m_value;
    public static Action operator *(Action a, double b) => new(a.m_value * b);
    public static Action operator *(Action a, Action b) => a * b.m_value;
    public static Action operator %(Action a, double b) => new(a.m_value % b);
    public static Action operator %(Action a, Action b) => a % b.m_value;
    public static Action operator -(Action a, double b) => new(a.m_value - b);
    public static Action operator -(Action a, Action b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Action o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Action other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ActionUnit.JouleSecond, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Action.Value"/> property is in <see cref="ActionUnit.JouleSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

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

    public static double GetUnitFactor(ActionUnit unit)
      => unit switch
      {
        ActionUnit.JouleSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ActionUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ActionUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ActionUnit.JouleSecond => preferUnicode ? "J\u22C5s" : "J·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ActionUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ActionUnit unit = ActionUnit.JouleSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
