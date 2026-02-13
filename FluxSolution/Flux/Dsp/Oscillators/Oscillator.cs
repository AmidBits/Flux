namespace Flux.Dsp.Oscillators
{
  public record class Oscillator
    : IOscillator
  {
    private double m_amplitudeModulation;
    /// <summary>The amount [0, 1] of output from the amplitude modulator to apply.</summary>
    public double AmplitudeModulation { get => m_amplitudeModulation; set => m_amplitudeModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The amplitude modulator (AM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Amplitude_modulation"/>
    public IOscillator? AmplitudeModulator { get; set; }

    /// <summary>The last sample generated.</summary>
    public double Current { get; private set; }

    private double m_frequency;
    /// <summary>The audio frequency in Hertz, i.e. the pitch, of the waveform. This is also the quotient of cycles/second.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Audio_frequency"/>
    public double Frequency { get => m_frequency; set => SetState(value, m_sampleRate); }

    private double m_frequencyModulation;
    /// <summary>The amount [0, 1] of output from the frequency modulator to apply.</summary>
    public double FrequencyModulation { get => m_frequencyModulation; set => m_frequencyModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The frequency modulator (FM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Frequency_modulation"/>
    public IOscillator? FrequencyModulator { get; set; }

    /// <summary>The wave generator used to produce the waveform of the oscillator.</summary>
    public WaveGenerators.IMonoWaveUiGeneratable? Generator { get; set; }

    /// <summary>Indicates whether the sample polarity should be inverted.</summary>
    public bool InvertPolarity { get; set; }

    /// <summary>The normalized frequency is also known as samples per cycle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    public double NormalizedFrequency { get; private set; }

    private double m_offset;
    /// <summary>Initial offset of the oscillator phase.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    public double Offset { get => m_offset; set => m_offset = Number.WrapAround(value, -1.0, 1.0); }

    private double m_phase;
    /// <summary>The position of a point in time (an instant) on the waveform cycle in the range [0, 1].</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    public double Phase { get => m_phase; set => m_phase = Number.WrapAround(value, 0.0, 1.0); }

    private double m_phaseModulation;
    /// <summary>The amount [0, 1] of output from the phase modulator to apply.</summary>
    public double PhaseModulation { get => m_phaseModulation; set => m_phaseModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The pulse modulator (PM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Phase_modulation"/>
    public Oscillator? PhaseModulator { get; set; }

    /// <summary>Audio processors applied before AM, RM, FM and PM.</summary>
    public System.Collections.Generic.List<WaveProcessors.IMonoWaveProcessable> PreProcessors { get; }

    /// <summary>Audio processors applied after AM, RM, FM and PM.</summary>
    public System.Collections.Generic.List<WaveProcessors.IMonoWaveProcessable> PostProcessors { get; }

    /// <summary>Indicates whether the direction of the phase should be reversed.</summary>
    public bool ReversePhase { get; set; }

    private double m_ringModulation;
    /// <summary>The amount [0, 1] of output from the ring modulator to apply.</summary>
    public double RingModulation { get => m_ringModulation; set => m_ringModulation = double.Clamp(value, 0.0, 1.0); }

    /// <summary>The ring modulator (RM) for the oscillator.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ring_modulation"/>
    public IOscillator? RingModulator { get; set; }

    /// <summary>The period of a sample, in seconds. Can be used to set the sample rate.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sampling_(signal_processing)#Theory"/>
    public double SamplePeriod { get; private set; }

    private double m_sampleRate;
    /// <summary>The sample rate, i.e. the resolution, of the waveform in hertz.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sampling_(signal_processing)#Sampling_rate"/>
    public double SampleRate { get => m_sampleRate; set => SetState(m_frequency, value); }

    /// <summary>The number of samples per cycle (in the period) of the waveform frequency.</summary>
    public double SamplesPerCycle { get; private set; }

    /// <summary>The period of the signal, in seconds. Can be used to set the frequency.</summary>
    public double SignalPeriod { get; private set; }

    public Oscillator(WaveGenerators.IMonoWaveUiGeneratable generator, double frequency, double sampleRate = 44100)
    {
      Generator = generator;

      PreProcessors = [];

      PostProcessors = [];

      Reset(false);

      SetState(frequency, sampleRate);
    }
    public Oscillator() : this(new WaveGenerators.SineWave(), 8.175798915643707, 44100) { }

    /// <summary>Generates the next sample for the oscillator (all operational components are integrated in ths process).</summary>
    public double Next(double? normalizedFrequency)
    {
      Current = Generator?.GenerateMonoWaveUi(m_phase).Wave ?? 0;

      if (InvertPolarity)
        Current = -Current;

      foreach (var processor in PreProcessors)
        Current = processor.ProcessMonoWave(new Waves.WaveMono<double>(Current)).Wave;

      if (AmplitudeModulator != null && m_amplitudeModulation > Tools.PositiveThreshold)
      {
        Current *= AmplitudeModulator.NextSample() * m_amplitudeModulation + 1;

        Current /= m_amplitudeModulation + 1; // Reset the amplitude after AM applied.
      }

      if (RingModulator != null && m_ringModulation > Tools.PositiveThreshold)
        Current *= RingModulator.NextSample() * m_ringModulation;

      foreach (var processor in PostProcessors)
        Current = processor.ProcessMonoWave(new Waves.WaveMono<double>(Current)).Wave;

      if (!normalizedFrequency.HasValue)
        normalizedFrequency = NormalizedFrequency;

      var phaseShift = normalizedFrequency.Value; // Normal phase shift for the current frequency.

      if (PhaseModulator != null && m_phaseModulation > Tools.PositiveThreshold)
        phaseShift += 0.1 * PhaseModulator.Next(normalizedFrequency.Value) * m_phaseModulation;

      if (FrequencyModulator != null && m_frequencyModulation > Tools.PositiveThreshold)
        phaseShift += normalizedFrequency.Value * FrequencyModulator.NextSample() * m_frequencyModulation;

      if (ReversePhase)
        phaseShift = -phaseShift;

      m_phase = IntervalNotation.Closed.WrapAround(m_phase + phaseShift, 0d, 1d); // Ensure phase wraps within cycle.

      System.Diagnostics.Debug.Assert(Current >= -1 && Current <= 1);

      return Current;
    }

    public double NextSample()
      => Next(null);

    /// <summary>Returns a sequence of the specified number of samples.</summary>
    public System.Collections.Generic.IEnumerable<double> GetNext(int count)
    {
      while (count-- > 1)
        yield return NextSample();
    }

    /// <summary>Resets the phase position using the phase offset. Can be used to "sync" the oscillator.</summary>
    public void Reset(bool resetModulators)
    {
      m_phase = m_offset;

      if (resetModulators)
      {
        (AmplitudeModulator as Oscillator)?.Reset(resetModulators);
        (FrequencyModulator as Oscillator)?.Reset(resetModulators);
        (PhaseModulator as Oscillator)?.Reset(resetModulators);
        (RingModulator as Oscillator)?.Reset(resetModulators);
      }
    }

    public void SetState(double frequency, double sampleRate)
    {
      m_frequency = frequency;
      m_sampleRate = sampleRate;

      NormalizedFrequency = m_frequency / m_sampleRate;

      SamplesPerCycle = m_sampleRate / m_frequency;

      SamplePeriod = 1.0 / m_sampleRate;
      SignalPeriod = 1.0 / m_frequency;
    }
  }
}
