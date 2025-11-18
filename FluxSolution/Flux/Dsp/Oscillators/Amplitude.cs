namespace Flux.Dsp.Oscillators
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Amplitude"/>
  public record class Amplitude
  {
    private double m_amplitudeModulation;
    /// <summary>The amount [0, 1] of output from the amplitude modulator to apply.</summary>
    public double AmplitudeModulation { get => m_amplitudeModulation; set => m_amplitudeModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The amplitude modulator (AM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Amplitude_modulation"/>
    public IOscillator? AmplitudeModulator { get; set; }

    /// <summary>Indicates whether the sample polarity should be inverted.</summary>
    public bool InvertPolarity { get; set; }

    public double Peak { get; private set; }

    public double Reference { get; private set; }

    private double m_ringModulation;
    /// <summary>The amount [0, 1] of output from the ring modulator to apply.</summary>
    public double RingModulation { get => m_ringModulation; set => m_ringModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The ring modulator (RM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ring_modulation"/>
    public IOscillator? RingModulator { get; set; }

    private readonly double m_maximumAmplitude;
    public double MaxAmplitude
      => m_maximumAmplitude;
    private readonly double m_minimumAmplitude;
    public double MinAmplitude
      => m_minimumAmplitude;

    //public ComputedRange PeakToPeak { get; private set; }

    public Amplitude(double peak, double reference)
    {
      Peak = peak;

      Reference = reference;

      m_minimumAmplitude = Reference - Peak;
      m_maximumAmplitude = Reference + Peak;
    }
    public Amplitude()
      : this(1.0, 0.0)
    { }

    public void Reset(bool resetModulators)
    {
      if (resetModulators)
      {
        (AmplitudeModulator as Oscillator)?.Reset(resetModulators);
        (RingModulator as Oscillator)?.Reset(resetModulators);
      }
    }

    public double Update(double sample)
    {
      if (InvertPolarity)
        sample = -sample;

      if (AmplitudeModulator != null && m_amplitudeModulation > SingleExtensions.MaxDefaultTolerance)
      {
        sample *= AmplitudeModulator.NextSample() * m_amplitudeModulation + 1.0;

        sample /= m_amplitudeModulation + 1.0; // Reset the amplitude after AM applied.
      }

      if (RingModulator != null && m_ringModulation > SingleExtensions.MaxDefaultTolerance)
        sample *= RingModulator.NextSample() * m_ringModulation;

      return sample;
    }
  }
}
