namespace Flux.Quantity
{
  public enum AmplitudeRatioUnit
  {
    DecibelVolt,
  }

  /// <summary>Amplitude ratio unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
#if NET5_0
  public struct AmplitudeRatio
    : System.IComparable<AmplitudeRatio>, System.IEquatable<AmplitudeRatio>, IValuedUnit<double>
#else
  public record struct AmplitudeRatio
    : System.IComparable<AmplitudeRatio>, IValuedUnit<double>
#endif
  {
    public const double ScalingFactor = 20;

    private readonly double m_value;

    public AmplitudeRatio(double value, AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt)
      => m_value = unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public PowerRatio ToPowerRatio()
      => new(System.Math.Pow(m_value, 2));

    public double ToUnitValue(AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt)
      => unit switch
      {
        AmplitudeRatioUnit.DecibelVolt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new AmplitudeRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public static AmplitudeRatio From(Voltage numerator, Voltage denominator)
      => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
    /// <summary>Creates a new AmplitudeRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
    /// <param name="decibelChange"></param>
    public static AmplitudeRatio FromDecibelChange(double decibelChange)
      => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AmplitudeRatio v)
      => v.m_value;
    public static explicit operator AmplitudeRatio(double v)
      => new(v);

    public static bool operator <(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(AmplitudeRatio a, AmplitudeRatio b)
      => a.Equals(b);
    public static bool operator !=(AmplitudeRatio a, AmplitudeRatio b)
      => !a.Equals(b);
#endif

    public static AmplitudeRatio operator -(AmplitudeRatio v)
      => new(-v.m_value);
    public static AmplitudeRatio operator +(AmplitudeRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b)
      => a + b.m_value;
    public static AmplitudeRatio operator /(AmplitudeRatio a, double b)
      => new(a.m_value - b);
    public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b)
      => a / b.m_value;
    public static AmplitudeRatio operator *(AmplitudeRatio a, double b)
      => new(a.m_value + b);
    public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b)
      => a * b.m_value;
    public static AmplitudeRatio operator -(AmplitudeRatio a, double b)
      => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
    public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AmplitudeRatio other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(AmplitudeRatio other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is AmplitudeRatio o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} dBV }}";
    #endregion Object overrides
  }
}
