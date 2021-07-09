namespace Flux.Units
{
  /// <summary>Temporal frequency.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Frequency"/>
  public struct Frequency
    : System.IComparable<Frequency>, System.IEquatable<Frequency>, IStandardizedScalar
  {
    private readonly double m_hertz;

    public Frequency(double hertz)
      => m_hertz = hertz;

    public double Hertz
      => m_hertz;

    /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>
    public Time ToPeriod()
      => new Time(1.0 / m_hertz);

    #region Static methods
    /// <summary>Creates a new Frequency instance from the specified acoustic properties of sound velocity and wavelength.</summary>
    /// <param name="soundVelocity"></param>
    /// <param name="wavelength"></param>
    public static Frequency ComputeFrequency(Speed soundVelocity, Length wavelength)
      => new Frequency(soundVelocity.MeterPerSecond / wavelength.Meter);
    /// <summary>Computes the normalized frequency (a.k.a. cycles/sample) of the specified frequency and sample rate. The normalized frequency represents a fractional part of the cycle, per sample.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    public static double ComputeNormalizedFrequency(double frequency, double sampleRate)
      => frequency / sampleRate;
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in cents.</summary>
    /// <param name="frequency"></param>
    /// <param name="cents"></param>
    public static Frequency ComputePitchShift(Frequency frequency, Cent cents)
      => new Frequency(frequency.Hertz * Cent.ConvertCentToFrequencyRatio(cents.Value));
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in semitones.</summary>
    /// <param name="frequency"></param>
    /// <param name="semitones"></param>
    public static Frequency ComputePitchShift(Frequency frequency, Semitone semitones)
      => new Frequency(frequency.Hertz * Semitone.ConvertSemitoneToFrequencyRatio(semitones.Value));
    /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>
    public static double ComputeSamplesPerCycle(double frequency, double sampleRate)
      => sampleRate / frequency;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator Frequency(double value)
      => new Frequency(value);
    public static explicit operator double(Frequency value)
      => value.m_hertz;

    public static bool operator <(Frequency a, Frequency b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Frequency a, Frequency b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Frequency a, Frequency b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Frequency a, Frequency b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Frequency a, Frequency b)
      => a.Equals(b);
    public static bool operator !=(Frequency a, Frequency b)
      => !a.Equals(b);

    public static Frequency operator -(Frequency v)
      => new Frequency(-v.m_hertz);
    public static Frequency operator +(Frequency a, Frequency b)
      => new Frequency(a.m_hertz + b.m_hertz);
    public static Frequency operator /(Frequency a, Frequency b)
      => new Frequency(a.m_hertz / b.m_hertz);
    public static Frequency operator %(Frequency a, Frequency b)
      => new Frequency(a.m_hertz % b.m_hertz);
    public static Frequency operator *(Frequency a, Frequency b)
      => new Frequency(a.m_hertz * b.m_hertz);
    public static Frequency operator -(Frequency a, Frequency b)
      => new Frequency(a.m_hertz - b.m_hertz);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Frequency other)
      => m_hertz.CompareTo(other.m_hertz);

    // IEquatable
    public bool Equals(Frequency other)
      => m_hertz == other.m_hertz;

    // IUnitStandardized
    public double GetScalar()
      => m_hertz;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Frequency o && Equals(o);
    public override int GetHashCode()
      => m_hertz.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_hertz} Hz>";
    #endregion Object overrides
  }
}
