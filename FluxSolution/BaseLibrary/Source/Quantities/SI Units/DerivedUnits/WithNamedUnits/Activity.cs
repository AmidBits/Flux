namespace Flux.Quantities
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
    : System.IComparable, System.IComparable<Activity>, System.IFormattable, ISiPrefixValueQuantifiable<double, ActivityUnit>
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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ActivityUnit.Becquerel, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ActivityUnit.Becquerel, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Activity.Value"/> property is in <see cref="ActivityUnit.Becquerel"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(ActivityUnit unit, double value)
      => unit switch
      {
        ActivityUnit.Becquerel => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ActivityUnit unit, double value)
      => unit switch
      {
        ActivityUnit.Becquerel => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(ActivityUnit unit)
      => unit switch
      {
        ActivityUnit.Becquerel => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ActivityUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ActivityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ActivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ActivityUnit unit)
      => unit switch
      {
        ActivityUnit.Becquerel => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(ActivityUnit unit = ActivityUnit.Becquerel, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
