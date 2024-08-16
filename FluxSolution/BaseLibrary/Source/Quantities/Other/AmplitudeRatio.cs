namespace Flux.Quantities
{
  public enum AmplitudeRatioUnit
  {
    /// <summary>This is the default unit for <see cref="AmplitudeRatio"/>.</summary>
    DecibelVolt,
  }

  /// <summary>
  /// <para>Amplitude ratio, unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Decibel"/></para>
  /// </summary>
  public readonly record struct AmplitudeRatio
    : System.IComparable, System.IComparable<AmplitudeRatio>, System.IFormattable, IUnitValueQuantifiable<double, AmplitudeRatioUnit>
  {
    public const double ScalingFactor = 20;

    private readonly double m_value;

    public AmplitudeRatio(double value, AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt)
      => m_value = unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public PowerRatio ToPowerRatio() => new(System.Math.Pow(m_value, 2));

    #region Static methods

    /// <summary>Creates a new AmplitudeRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public static AmplitudeRatio From(Voltage numerator, Voltage denominator) => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
    /// <summary>Creates a new AmplitudeRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
    /// <param name="decibelChange"></param>
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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AmplitudeRatioUnit.DecibelVolt, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="AmplitudeRatio.Value"/> property is in <see cref="AmplitudeRatioUnit.DecibelVolt"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AmplitudeRatioUnit unit, bool preferPlural)
      => unit.ToString() + GetUnitValue(unit).PluralStringSuffix();

    public string GetUnitSymbol(AmplitudeRatioUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AmplitudeRatioUnit.DecibelVolt => "dBV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AmplitudeRatioUnit unit)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetUnitName(unit, preferPlural));
      return sb.ToString();
    }

    public string ToUnitValueSymbolString(AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetUnitSymbol(unit, preferUnicode));
      return sb.ToString();
    }

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
