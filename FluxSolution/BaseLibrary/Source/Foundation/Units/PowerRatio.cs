namespace Flux.Units
{
  /// <summary>Power ratio, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
  public struct PowerRatio
    : System.IComparable<PowerRatio>, System.IEquatable<PowerRatio>, IStandardizedScalar
  {
    public const double ScalingFactor = 10;

    private readonly double m_decibelWatt;

    public PowerRatio(double decibelWatt)
      => m_decibelWatt = decibelWatt;

    public double DecibelWatt
      => m_decibelWatt;

    public AmplitudeRatio ToAmplitudeRatio()
      => new AmplitudeRatio(System.Math.Sqrt(m_decibelWatt));

    #region Static methods
    public static PowerRatio FromDecibelChange(double decibelChange)
      => new PowerRatio(System.Math.Pow(10, decibelChange / ScalingFactor)); // Pow inverse of Log10.
    public static PowerRatio FromPowerRatio(Power numerator, Power denominator)
      => new PowerRatio(ScalingFactor * System.Math.Log10(numerator.Watt / denominator.Watt));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(PowerRatio v)
      => v.m_decibelWatt;
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
      => new PowerRatio(-v.m_decibelWatt);
    public static PowerRatio operator +(PowerRatio a, PowerRatio b)
      => new PowerRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_decibelWatt / ScalingFactor) + System.Math.Pow(10, b.m_decibelWatt / ScalingFactor)));
    public static PowerRatio operator /(PowerRatio a, PowerRatio b)
      => new PowerRatio(a.m_decibelWatt - b.m_decibelWatt);
    public static PowerRatio operator *(PowerRatio a, PowerRatio b)
      => new PowerRatio(a.m_decibelWatt + b.m_decibelWatt);
    public static PowerRatio operator -(PowerRatio a, PowerRatio b)
      => new PowerRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_decibelWatt / ScalingFactor) - System.Math.Pow(10, b.m_decibelWatt / ScalingFactor)));
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(PowerRatio other)
      => m_decibelWatt.CompareTo(other.m_decibelWatt);

    // IEquatable
    public bool Equals(PowerRatio other)
      => m_decibelWatt == other.m_decibelWatt;

    // IUnitStandardized
    public double GetScalar()
      => m_decibelWatt;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is PowerRatio o && Equals(o);
    public override int GetHashCode()
      => m_decibelWatt.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_decibelWatt} dBW>";
    #endregion Object overrides
  }
}
