namespace Flux.Units
{
  /// <summary>Amplitude ratio, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
  public struct AmplitudeRatio
    : System.IComparable<AmplitudeRatio>, System.IEquatable<AmplitudeRatio>, IStandardizedScalar
  {
    public const double ScalingFactor = 20;

    private readonly double m_decibelVolt;

    public AmplitudeRatio(double decibelVolt)
      => m_decibelVolt = decibelVolt;

    public double DecibelVolt
      => m_decibelVolt;

    public PowerRatio ToPowerRatio()
      => new PowerRatio(System.Math.Pow(m_decibelVolt, 2));

    #region Static methods
    public static AmplitudeRatio Add(AmplitudeRatio left, AmplitudeRatio right)
      => new AmplitudeRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, left.m_decibelVolt / ScalingFactor) + System.Math.Pow(10, right.m_decibelVolt / ScalingFactor))); // Pow inverse of Log10.
    public static AmplitudeRatio Divide(AmplitudeRatio left, AmplitudeRatio right)
      => new AmplitudeRatio(left.m_decibelVolt - right.m_decibelVolt);
    public static AmplitudeRatio FromAmplitudeRatio(Voltage numerator, Voltage denominator)
      => new AmplitudeRatio(ScalingFactor * System.Math.Log10(numerator.Volt / denominator.Volt));
    public static AmplitudeRatio FromDecibelChange(double decibelChange)
      => new AmplitudeRatio(System.Math.Pow(10, decibelChange / ScalingFactor)); // Pow inverse of Log10.
    public static AmplitudeRatio Multiply(AmplitudeRatio left, AmplitudeRatio right)
      => new AmplitudeRatio(left.m_decibelVolt + right.m_decibelVolt);
    public static AmplitudeRatio Negate(AmplitudeRatio value)
      => new AmplitudeRatio(-value.m_decibelVolt);
    public static AmplitudeRatio Subtract(AmplitudeRatio left, AmplitudeRatio right)
      => new AmplitudeRatio(ScalingFactor * System.Math.Log10(System.Math.Pow(10, left.m_decibelVolt / ScalingFactor) - System.Math.Pow(10, right.m_decibelVolt / ScalingFactor)));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AmplitudeRatio v)
      => v.m_decibelVolt;
    public static explicit operator AmplitudeRatio(double v)
      => new AmplitudeRatio(v);

    public static bool operator <(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(AmplitudeRatio a, AmplitudeRatio b)
      => a.Equals(b);
    public static bool operator !=(AmplitudeRatio a, AmplitudeRatio b)
      => !a.Equals(b);

    public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b)
      => Add(a, b);
    public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b)
      => Divide(a, b);
    public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b)
      => Multiply(a, b);
    public static AmplitudeRatio operator -(AmplitudeRatio v)
      => Negate(v);
    public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AmplitudeRatio other)
      => m_decibelVolt.CompareTo(other.m_decibelVolt);

    // IEquatable
    public bool Equals(AmplitudeRatio other)
      => m_decibelVolt == other.m_decibelVolt;

    // IUnitStandardized
    public double GetScalar()
      => m_decibelVolt;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AmplitudeRatio o && Equals(o);
    public override int GetHashCode()
      => m_decibelVolt.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_decibelVolt} dBV>";
    #endregion Object overrides
  }
}
