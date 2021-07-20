namespace Flux.Units
{
  /// <summary>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
  public struct PowerRatio
    : System.IComparable<PowerRatio>, System.IEquatable<PowerRatio>, IStandardizedScalar
  {
    public const double ScalingFactor = 10;

    private readonly double m_value;

    public PowerRatio(double decibelWatt)
      => m_value = decibelWatt;

    public double Value
      => m_value;

    public AmplitudeRatio ToAmplitudeRatio()
      => new AmplitudeRatio(System.Math.Sqrt(m_value));

    #region Static methods
    /// <summary>Creates a new PowerRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public static PowerRatio From(Power numerator, Power denominator)
      => new PowerRatio(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
    /// <summary>Creates a new PowerRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
    /// <param name="decibelChange"></param>
    public static PowerRatio FromDecibelChange(double decibelChange)
      => new PowerRatio(System.Math.Pow(10, decibelChange / ScalingFactor)); // Pow inverse of Log10.
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(PowerRatio v)
      => v.m_value;
    public static explicit operator PowerRatio(double v)
      => new PowerRatio(v);

    public static bool operator <(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(PowerRatio a, PowerRatio b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(PowerRatio a, PowerRatio b)
      => a.Equals(b);
    public static bool operator !=(PowerRatio a, PowerRatio b)
      => !a.Equals(b);

    public static PowerRatio operator -(PowerRatio v)
      => new PowerRatio(-v.m_value);
    public static PowerRatio operator +(PowerRatio a, PowerRatio b)
      => new PowerRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b.m_value / ScalingFactor)));
    public static PowerRatio operator /(PowerRatio a, PowerRatio b)
      => new PowerRatio(a.m_value - b.m_value);
    public static PowerRatio operator *(PowerRatio a, PowerRatio b)
      => new PowerRatio(a.m_value + b.m_value);
    public static PowerRatio operator -(PowerRatio a, PowerRatio b)
      => new PowerRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b.m_value / ScalingFactor)));
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(PowerRatio other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(PowerRatio other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is PowerRatio o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} dBW>";
    #endregion Object overrides
  }
}
