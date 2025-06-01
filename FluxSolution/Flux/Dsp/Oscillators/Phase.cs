namespace Flux.Dsp.Oscillators
{
  public record class Phase
  {
    private double m_frequencyModulation;
    /// <summary>The amount [0, 1] of output from the frequency modulator to apply.</summary>
    public double FrequencyModulation { get => m_frequencyModulation; set => m_frequencyModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The frequency modulator (FM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Frequency_modulation"/>
    public IOscillator? FrequencyModulator { get; set; }

    public double NormalizedFrequency { get; set; }

    private double m_offset;
    public double Offset { get => m_offset; set => m_offset = value.WrapAroundClosed(m_minimumPhase, m_maximumPhase); }

    private double m_phaseModulation;
    /// <summary>The amount [0, 1] of output from the phase modulator to apply.</summary>
    public double PhaseModulation { get => m_phaseModulation; set => m_phaseModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The pulse modulator (PM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_modulation"/>
    public Oscillator? PhaseModulator { get; set; }

    private double m_position;
    public double Position { get => m_position; set => m_position = value.WrapAroundClosed(m_minimumPhase, m_maximumPhase); }

    private readonly double m_maximumPhase;
    private readonly double m_minimumPhase;

    /// <summary>Indicates whether the phase was reset, i.e. the cycle was completed.</summary>
    public bool WasReset { get; set; }

    /// <summary>Indicates whether the direction of the phase should be reversed.</summary>
    public bool Reverse { get; set; }

    public Phase(double normalizedFrequency)
    {
      NormalizedFrequency = normalizedFrequency;

      m_minimumPhase = 0;
      m_maximumPhase = 1;

      m_position = m_offset = 0.0;
    }
    public Phase()
      : this(440.0 / 44100.0)
    { }

    /// <summary>Resets the phase position using the phase offset. Can be used to "sync" the oscillator.</summary>
    public void Reset(bool resetModulators)
    {
      m_position = m_offset;

      if (resetModulators)
      {
        (FrequencyModulator as Oscillator)?.Reset(resetModulators);
        (PhaseModulator as Oscillator)?.Reset(resetModulators);
      }
    }

    public void Update(double? normalizedFrequency)
    {
      if (!normalizedFrequency.HasValue)
        normalizedFrequency = NormalizedFrequency;

      var shift = normalizedFrequency.Value; // Normal phase shift for the current frequency.

      if (PhaseModulator != null && m_phaseModulation > Numerics.Constants.EpsilonCpp32)
        shift += 0.1 * PhaseModulator.Next(normalizedFrequency.Value) * m_phaseModulation;

      if (FrequencyModulator != null && m_frequencyModulation > Numerics.Constants.EpsilonCpp32)
        shift += normalizedFrequency.Value * FrequencyModulator.NextSample() * m_frequencyModulation;

      if (Reverse)
        shift = -shift;

      m_position = (m_position + shift).WrapAroundClosed(m_minimumPhase, m_maximumPhase);
    }
  }
}
