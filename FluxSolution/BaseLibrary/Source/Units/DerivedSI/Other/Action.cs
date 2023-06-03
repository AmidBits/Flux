namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static string GetUnitString(this Units.ActionUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ActionUnit.JouleSecond => preferUnicode ? "J\u22C5s" : "J·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ActionUnit
    {
      JouleSecond,
    }

    /// <summary>Action. Unit of Joule second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Action_(physics)"/>
    public readonly record struct Action
      : System.IComparable, System.IComparable<Action>, System.IFormattable, IUnitQuantifiable<double, ActionUnit>
    {
      public const ActionUnit DefaultUnit = ActionUnit.JouleSecond;

      public static readonly Action PlanckConstant = new(6.62607015e-34);

      private readonly double m_value;

      public Action(double value, ActionUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ActionUnit.JouleSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Action v) => v.m_value;
      public static explicit operator Action(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(ActionUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(ActionUnit unit = DefaultUnit)
        => unit switch
        {
          ActionUnit.JouleSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
