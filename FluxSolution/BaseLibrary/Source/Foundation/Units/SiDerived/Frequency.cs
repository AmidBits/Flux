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

    #region Static methods
    public static Frequency Add(Frequency left, Frequency right)
      => new Frequency(left.m_hertz + right.m_hertz);
    public static Frequency Divide(Frequency left, Frequency right)
      => new Frequency(left.m_hertz / right.m_hertz);
    public static Frequency FromAcoustics(Speed soundVelocity, Length waveLength)
      => new Frequency(soundVelocity.MeterPerSecond / waveLength.Meter);
    public static Frequency Multiply(Frequency left, Frequency right)
      => new Frequency(left.m_hertz * right.m_hertz);
    public static Frequency Negate(Frequency frequency)
      => new Frequency(-frequency.m_hertz);
    public static Frequency Remainder(Frequency dividend, Frequency divisor)
      => new Frequency(dividend.m_hertz % divisor.m_hertz);
    /// <summary>Computes the normalized frequency of the specified frequency and sample rate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    public static double GetNormalizedFrequency(double frequency, double sampleRate)
      => frequency / sampleRate;
    /// <summary>Returns the phase speed (in meters per second, a.k.a. m/s) at the specified frequency and wavelength.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_velocity"/>
    public static double GetPhaseVelocity(double frequency, double waveLength)
      => frequency * waveLength;
    /// <summary>Computes the time (in seconds) it takes to complete one cycle at the specified frequency.</summary>
    public static double GetPeriod(double frequency)
      => 1.0 / frequency;
    /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>
    public static double GetSamplesPerCycle(double frequency, double sampleRate)
      => sampleRate / frequency;
    /// <summary>The wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per seond) for the wavelength distance.</summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If sound waves then typically speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <returns>The wave length in the unit specified (default is in meters per second, i.e. 343.21 m/s).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Wavelength"/>
    public static double GetWaveLength(double frequency, double phaseVelocity)
      => phaseVelocity / frequency;
    public static Frequency Subtract(Frequency left, Frequency right)
      => new Frequency(left.m_hertz - right.m_hertz);
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

    public static Frequency operator +(Frequency a, Frequency b)
      => Add(a, b);
    public static Frequency operator /(Frequency a, Frequency b)
      => Divide(a, b);
    public static Frequency operator %(Frequency a, Frequency b)
      => Remainder(a, b);
    public static Frequency operator *(Frequency a, Frequency b)
      => Multiply(a, b);
    public static Frequency operator -(Frequency a, Frequency b)
      => Subtract(a, b);
    public static Frequency operator -(Frequency v)
      => Negate(v);
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
