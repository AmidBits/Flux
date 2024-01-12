namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ActivityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ActivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ActivityUnit
    {
      /// <summary>Becquerel.</summary>
      Becquerel,
    }

    /// <summary>Activity, unit of becquerel.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Specific_activity"/>
    public readonly record struct Activity
      : System.IComparable, System.IComparable<Activity>, IUnitValueQuantifiable<double, ActivityUnit>
    {
      public const ActivityUnit DefaultUnit = ActivityUnit.Becquerel;

      private readonly double m_value;

      public Activity(double value, ActivityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ActivityUnit.Becquerel => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Activity v) => v.m_value;
      public static explicit operator Activity(double v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ActivityUnit unit)
        => unit switch
        {
          ActivityUnit.Becquerel => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ActivityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
