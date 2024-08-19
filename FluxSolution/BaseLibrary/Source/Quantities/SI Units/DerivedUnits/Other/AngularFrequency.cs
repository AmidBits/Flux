namespace Flux.Quantities
{
  public enum AngularFrequencyUnit
  {
    /// <summary>This is the default unit for <see cref="AngularFrequency"/>.</summary>
    RadianPerSecond,
  }

  /// <summary>Angular frequency (a.k.a. angular speed, angular rate), unit of radians per second. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Angular_frequency"/>
  public readonly record struct AngularFrequency
    : System.IComparable, System.IComparable<AngularFrequency>, System.IFormattable, IUnitValueQuantifiable<double, AngularFrequencyUnit>
  {
    private readonly double m_value;

    public AngularFrequency(double value, AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond)
      => m_value = unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>Creates a new <see cref="AngularFrequency"/> instance from <see cref="Speed">tangential/linear speed</see> and <see cref="Length">radius</see></summary>
    public AngularFrequency(Speed tangentialSpeed, Length radius) : this(tangentialSpeed.Value / radius.Value) { }

    public Frequency ToFrequency() => new(m_value / System.Math.Tau);

    #region Static methods

    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
    /// </summary>
    /// <param name="radianPerSecond"></param>
    /// <returns></returns>
    public static double ConvertAngularVelocityToRpm(double radianPerSecond) => radianPerSecond / System.Math.Tau;

    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>
    /// </summary>
    /// <param name="revolutionPerMinute"></param>
    /// <returns></returns>
    public static double ConvertRpmToAngularVelocity(double revolutionPerMinute) => revolutionPerMinute / 60;

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) < 0;
    public static bool operator <=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) <= 0;
    public static bool operator >(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) > 0;
    public static bool operator >=(AngularFrequency a, AngularFrequency b) => a.CompareTo(b) >= 0;

    public static AngularFrequency operator -(AngularFrequency v) => new(-v.m_value);
    public static AngularFrequency operator +(AngularFrequency a, AngularFrequency b) => new(a.m_value + b.m_value);
    public static AngularFrequency operator /(AngularFrequency a, AngularFrequency b) => new(a.m_value / b.m_value);
    public static AngularFrequency operator *(AngularFrequency a, AngularFrequency b) => new(a.m_value * b.m_value);
    public static AngularFrequency operator %(AngularFrequency a, AngularFrequency b) => new(a.m_value % b.m_value);
    public static AngularFrequency operator -(AngularFrequency a, AngularFrequency b) => new(a.m_value - b.m_value);

    public static AngularFrequency operator +(AngularFrequency a, double b) => new(a.m_value + b);
    public static AngularFrequency operator /(AngularFrequency a, double b) => new(a.m_value / b);
    public static AngularFrequency operator *(AngularFrequency a, double b) => new(a.m_value * b);
    public static AngularFrequency operator %(AngularFrequency a, double b) => new(a.m_value % b);
    public static AngularFrequency operator -(AngularFrequency a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AngularFrequency o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AngularFrequency other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AngularFrequencyUnit.RadianPerSecond, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="AngularFrequency.Value"/> property is in <see cref="AngularFrequencyUnit.RadianPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AngularFrequencyUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(AngularFrequencyUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AngularFrequencyUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };


    public double GetUnitValue(AngularFrequencyUnit unit)
      => unit switch
      {
        AngularFrequencyUnit.RadianPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(AngularFrequencyUnit unit = AngularFrequencyUnit.RadianPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
