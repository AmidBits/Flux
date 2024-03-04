namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ActionUnit unit, bool preferUnicode = true, bool useFullName = false)
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
        => ToUnitValueString(ActionUnit.JouleSecond, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
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

      public string ToUnitValueString(ActionUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(ActionUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
