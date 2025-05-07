namespace Flux.Units
{
  /// <summary>
  /// <para>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Decibel"/></para>
  /// </summary>
  public readonly record struct PowerRatio
    : System.IComparable, System.IComparable<PowerRatio>, System.IFormattable, IUnitValueQuantifiable<double, PowerRatioUnit>
  {
    public const double ScalingFactor = 10;

    private readonly double m_value;

    public PowerRatio(double value, PowerRatioUnit unit = PowerRatioUnit.DecibelWatt) => m_value = ConvertFromUnit(unit, value);

    public PowerRatio(MetricPrefix prefix, double decibelWatt) => m_value = prefix.ChangePrefix(decibelWatt, MetricPrefix.Unprefixed);

    public AmplitudeRatio ToAmplitudeRatio() => new(double.Sqrt(m_value));

    #region Static methods

    /// <summary>
    /// <para>Creates a new PowerRatio instance from the difference of the specified voltages (numerator and denominator).</para>
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static PowerRatio From(Power numerator, Power denominator) => new(ScalingFactor * double.Log10(numerator.Value / denominator.Value));

    /// <summary>
    /// <para>Creates a new PowerRatio instance from the specified decibel change (i.e. a decibel interval).</para>
    /// </summary>
    /// <param name="decibelChange"></param>
    /// <returns></returns>
    public static PowerRatio FromDecibelChange(double decibelChange) => new(double.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(PowerRatio a, PowerRatio b) => a.CompareTo(b) < 0;
    public static bool operator <=(PowerRatio a, PowerRatio b) => a.CompareTo(b) <= 0;
    public static bool operator >(PowerRatio a, PowerRatio b) => a.CompareTo(b) > 0;
    public static bool operator >=(PowerRatio a, PowerRatio b) => a.CompareTo(b) >= 0;

    public static PowerRatio operator -(PowerRatio v) => new(-v.m_value);
    public static PowerRatio operator +(PowerRatio a, double b) => new(ScalingFactor * double.Log10(double.Pow(10, a.m_value / ScalingFactor) + double.Pow(10, b / ScalingFactor)));
    public static PowerRatio operator +(PowerRatio a, PowerRatio b) => a + b.m_value;
    public static PowerRatio operator /(PowerRatio a, double b) => new(a.m_value - b);
    public static PowerRatio operator /(PowerRatio a, PowerRatio b) => a / b.m_value;
    public static PowerRatio operator *(PowerRatio a, double b) => new(a.m_value + b);
    public static PowerRatio operator *(PowerRatio a, PowerRatio b) => a * b.m_value;
    public static PowerRatio operator -(PowerRatio a, double b) => new(ScalingFactor * double.Log10(double.Pow(10, a.m_value / ScalingFactor) - double.Pow(10, b / ScalingFactor)));
    public static PowerRatio operator -(PowerRatio a, PowerRatio b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is PowerRatio o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(PowerRatio other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(PowerRatioUnit.DecibelWatt, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="PowerRatio.Value"/> property is in <see cref="PowerRatioUnit.DecibelWatt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PowerRatioUnit unit, double value)
      => unit switch
      {
        PowerRatioUnit.DecibelWatt => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(PowerRatioUnit unit, double value)
      => unit switch
      {
        PowerRatioUnit.DecibelWatt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, PowerRatioUnit from, PowerRatioUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(PowerRatioUnit unit)
      => unit switch
      {
        PowerRatioUnit.DecibelWatt => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(PowerRatioUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(PowerRatioUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.PowerRatioUnit.DecibelWatt => "dBW",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PowerRatioUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PowerRatioUnit unit = PowerRatioUnit.DecibelWatt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
