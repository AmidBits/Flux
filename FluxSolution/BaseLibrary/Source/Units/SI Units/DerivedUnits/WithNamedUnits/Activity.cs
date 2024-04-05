namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.ActivityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ActivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ActivityUnit
    {
      /// <summary>This is the default unit for <see cref="Activity"/>.</summary>
      Becquerel,
    }

    /// <summary>
    /// <para>Activity, unit of becquerel.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Specific_activity"/></para>
    /// </summary>
    public readonly record struct Activity
      : System.IComparable, System.IComparable<Activity>, System.IFormattable, IUnitValueQuantifiable<double, ActivityUnit>
    {
      private readonly double m_value;

      public Activity(double value, ActivityUnit unit = ActivityUnit.Becquerel)
        => m_value = unit switch
        {
          ActivityUnit.Becquerel => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(Activity a, Activity b) => a.CompareTo(b) < 0;
      public static bool operator <=(Activity a, Activity b) => a.CompareTo(b) <= 0;
      public static bool operator >(Activity a, Activity b) => a.CompareTo(b) > 0;
      public static bool operator >=(Activity a, Activity b) => a.CompareTo(b) >= 0;

      public static Activity operator -(Activity v) => new(-v.m_value);
      public static Activity operator +(Activity a, double b) => new(a.m_value + b);
      public static Activity operator +(Activity a, Activity b) => a + b.m_value;
      public static Activity operator /(Activity a, double b) => new(a.m_value / b);
      public static Activity operator /(Activity a, Activity b) => a / b.m_value;
      public static Activity operator *(Activity a, double b) => new(a.m_value * b);
      public static Activity operator *(Activity a, Activity b) => a * b.m_value;
      public static Activity operator %(Activity a, double b) => new(a.m_value % b);
      public static Activity operator %(Activity a, Activity b) => a % b.m_value;
      public static Activity operator -(Activity a, double b) => new(a.m_value - b);
      public static Activity operator -(Activity a, Activity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Activity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Activity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ActivityUnit.Becquerel, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Activity.Value"/> property is in <see cref="ActivityUnit.Becquerel"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(ActivityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(ActivityUnit unit)
        => unit switch
        {
          ActivityUnit.Becquerel => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ActivityUnit unit = ActivityUnit.Becquerel, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
