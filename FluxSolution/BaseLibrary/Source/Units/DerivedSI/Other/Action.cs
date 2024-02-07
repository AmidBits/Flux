namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ActionUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ActionUnit.JouleSecond => options.PreferUnicode ? "J\u22C5s" : "J·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="Action.Value"/> property is in <see cref="ActionUnit.JouleSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ActionUnit unit)
        => unit switch
        {
          ActionUnit.JouleSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ActionUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
