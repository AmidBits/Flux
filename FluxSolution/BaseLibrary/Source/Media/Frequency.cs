namespace Flux.Media
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Frequency
    : System.IEquatable<Frequency>, System.IFormattable
  {
    public const double Reference440 = 440;

    private static readonly double FrequencyRatioOfCent = System.Math.Pow(2.0, 1.0 / 1200.0);

    private readonly double m_hertz;

    public Frequency(double hertz)
      => m_hertz = hertz >= 0 ? hertz : throw new System.ArgumentOutOfRangeException(nameof(hertz));

    public double Hertz 
      => m_hertz;

    #region Statics
    /// <summary>Computes the normalized frequency is a unit of measurement of frequency equivalent to cycles/sample. In digital signal processing (DSP), the continuous time variable, t, with units of seconds, is replaced by the discrete integer variable, n, with units of samples. More precisely, the time variable, in seconds, has been normalized (divided) by the sampling interval, T (seconds/sample), which causes time to have convenient integer values at the moments of sampling.</summary>
    public static double CyclesPerSample(double frequency, double sampleRate)
      => frequency / sampleRate;

    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public static Frequency FromMidiNote(int midiNote)
      => midiNote >= 0 && midiNote <= 127 && Reference440 * System.Math.Pow(2, (midiNote - Midi.Note.ReferenceA4) / 12.0) is var hertz ? new Frequency(hertz) : throw new System.ArgumentOutOfRangeException(nameof(midiNote));
    public static Frequency FromMidiNote(Midi.Note midiNote)
      => FromMidiNote(midiNote.Number);

    /// <summary>The number of samples in one complete frequency cycle.</summary>
    public static double SamplesPerCycle(double frequency, double sampleRate)
      => sampleRate / frequency;

    /// <summary>Computes the number of milliseconds per period of the specified frequency.</summary>
    public static double SecondsPerPeriod(double frequency)
      => 1.0 / frequency;

    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int cents)
      => (System.Math.Pow(Music.Interval.Cent.FrequencyRatio, cents) * frequency);

    /// <summary>The wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per seond) for the wavelength distance.</summary>
    /// <returns>The wave length in the unit specified (default is in meters per second, i.e. 343.21 m/s).</returns>
    public static double ToWaveLength(double frequency, double speedOfSound = 343.21)
      => speedOfSound / frequency;
    #endregion Statics

    // Operators
    public static bool operator ==(Frequency a, Frequency b)
  => a.Equals(b);
    public static bool operator !=(Frequency a, Frequency b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(Frequency other)
      => m_hertz == other.m_hertz;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Frequency)}: {{0:D3}}>", this);
    // Overrides
    public override bool Equals(object? obj)
      => obj is Frequency o && Equals(o);
    public override int GetHashCode()
      => m_hertz.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}
