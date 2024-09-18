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

    public Action(double value, ActionUnit unit = ActionUnit.JouleSecond)
      => m_value = unit switch
      {
        ActionUnit.JouleSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(ActionUnit.JouleSecond, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Action.Value"/> property is in <see cref="ActionUnit.JouleSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(ActionUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(ActionUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ActionUnit.JouleSecond => preferUnicode ? "J\u22C5s" : "J·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ActionUnit unit)
      => unit switch
      {
        ActionUnit.JouleSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(ActionUnit unit = ActionUnit.JouleSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(ActionUnit unit = ActionUnit.JouleSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider)+ unitSpacing.ToSpacingString()+ GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
