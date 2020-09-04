using System.Linq;

namespace Flux.Dsp
{
  public class PhaseEngine
  {
    private readonly double[] m_midiNotePhaseShift;

    private readonly double m_phaseLength;

    private readonly int m_sampleRate;

    public PhaseEngine(double phaseLength, int sampleRate)
    {
      m_phaseLength = phaseLength;

      m_sampleRate = sampleRate;

      m_midiNotePhaseShift = System.Linq.Enumerable.Range(0, 128).Select(note => m_phaseLength * Media.Midi.Note.ToFrequency((byte)note) / sampleRate).ToArray();
    }
    public PhaseEngine()
      : this(Maths.PiX2, 44100)
    {
    }

    public double UpdatePhase(double phasePosition, int midiNote)
      => midiNote >= 0 && midiNote <= 127 ? (phasePosition + m_midiNotePhaseShift[midiNote]) % m_phaseLength : throw new System.ArgumentOutOfRangeException(nameof(midiNote));

    public double UpdatePhase(double phasePosition, double arbitraryFrequency)
      => (phasePosition + (m_phaseLength * arbitraryFrequency / m_sampleRate)) % m_phaseLength;
  }

  public class Phase
  {
    private double m_frequencyModulation;
    /// <summary>The amount [0, 1] of output from the frequency modulator to apply.</summary>
    public double FrequencyModulation { get => m_frequencyModulation; set => m_frequencyModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The frequency modulator (FM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Frequency_modulation"/>
    public IOscillator? FrequencyModulator { get; set; }

    public double NormalizedFrequency { get; set; }

    private double m_offset;
    public double Offset { get => m_offset; set => m_offset = Maths.Wrap(value, m_minimumPhase, m_maximumPhase); }

    private double m_phaseModulation;
    /// <summary>The amount [0, 1] of output from the phase modulator to apply.</summary>
    public double PhaseModulation { get => m_phaseModulation; set => m_phaseModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The pulse modulator (PM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_modulation"/>
    public Oscillator? PhaseModulator { get; set; }

    private double m_position;
    public double Position { get => m_position; set => m_position = Maths.Wrap(value, m_minimumPhase, m_maximumPhase); }

    private double m_maximumPhase;
    private double m_minimumPhase;

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
    public Phase() : this(Media.Frequency.Normalized(440.0, 44100.0)) { }

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
      {
        normalizedFrequency = NormalizedFrequency;
      }

      var shift = normalizedFrequency.Value; // Normal phase shift for the current frequency.

      if (PhaseModulator != null && m_phaseModulation > Flux.Maths.EpsilonCpp32)
      {
        shift += 0.1 * PhaseModulator.Next(normalizedFrequency.Value) * m_phaseModulation;
      }

      if (FrequencyModulator != null && m_frequencyModulation > Flux.Maths.EpsilonCpp32)
      {
        shift += normalizedFrequency.Value * FrequencyModulator.NextSample() * m_frequencyModulation;
      }

      if (Reverse)
      {
        shift = -shift;
      }

      m_position = Maths.Wrap(m_position + shift, m_minimumPhase, m_maximumPhase);
    }
  }
}
