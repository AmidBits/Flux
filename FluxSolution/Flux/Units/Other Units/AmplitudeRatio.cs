namespace Flux.Units
{
  /// <summary>
  /// <para>Amplitude ratio, unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Decibel"/></para>
  /// </summary>
  public readonly record struct AmplitudeRatio
    : System.IComparable, System.IComparable<AmplitudeRatio>, System.IFormattable, IUnitValueQuantifiable<double, AmplitudeRatioUnit>
  {
    public const double ScalingFactor = 20;

    private readonly double m_value;

    public AmplitudeRatio(double value, AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt) => m_value = ConvertFromUnit(unit, value);

    public AmplitudeRatio(MetricPrefix prefix, double decibelVolt) => m_value = prefix.ConvertTo(decibelVolt, MetricPrefix.Unprefixed);

    public PowerRatio ToPowerRatio() => new(System.Math.Pow(m_value, 2));

    #region Static methods

    /// <summary>
    /// <para>Creates a new AmplitudeRatio instance from the difference of the specified voltages (numerator and denominator).</para>
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static AmplitudeRatio From(ElectricPotential numerator, ElectricPotential denominator) => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));

    /// <summary>
    /// <para>Creates a new AmplitudeRatio instance from the specified decibel change (i.e. a decibel interval).</para>
    /// </summary>
    /// <param name="decibelChange"></param>
    /// <returns></returns>
    public static AmplitudeRatio FromDecibelChange(double decibelChange) => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) < 0;
    public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) <= 0;
    public static bool operator >(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) > 0;
    public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) >= 0;

    public static AmplitudeRatio operator -(AmplitudeRatio v) => new(-v.m_value);
    public static AmplitudeRatio operator +(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b) => a + b.m_value;
    public static AmplitudeRatio operator /(AmplitudeRatio a, double b) => new(a.m_value - b);
    public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b) => a / b.m_value;
    public static AmplitudeRatio operator *(AmplitudeRatio a, double b) => new(a.m_value + b);
    public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b) => a * b.m_value;
    public static AmplitudeRatio operator -(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AmplitudeRatio o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AmplitudeRatio other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AmplitudeRatioUnit.DecibelVolt, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AmplitudeRatio.Value"/> property is in <see cref="AmplitudeRatioUnit.DecibelVolt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AmplitudeRatioUnit unit, double value)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(AmplitudeRatioUnit unit, double value)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AmplitudeRatioUnit from, AmplitudeRatioUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AmplitudeRatioUnit unit)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AmplitudeRatioUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(AmplitudeRatioUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.AmplitudeRatioUnit.DecibelVolt => "dBV",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AmplitudeRatioUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
