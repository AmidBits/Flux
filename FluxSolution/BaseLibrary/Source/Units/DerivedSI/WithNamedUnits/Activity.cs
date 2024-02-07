namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ActivityUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ActivityUnit.Becquerel => options.PreferUnicode ? "\u33C3" : "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ActivityUnit
    {
      /// <summary>This is the default unit for <see cref="Activity"/>.</summary>
      Becquerel,
    }

    /// <summary>Activity, unit of becquerel.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Specific_activity"/>
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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(ActivityUnit.Becquerel, options);

      /// <summary>
      /// <para>The unit of the <see cref="Activity.Value"/> property is in <see cref="ActivityUnit.Becquerel"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ActivityUnit unit)
        => unit switch
        {
          ActivityUnit.Becquerel => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ActivityUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
