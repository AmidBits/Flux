namespace Flux.Dsp
{
  public class Oscillator : IOscillator
  {
    private double _amplitudeModulation;
    /// <summary>The amount [0, 1] of output from the amplitude modulator to apply.</summary>
    public double AmplitudeModulation { get => _amplitudeModulation; set => _amplitudeModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The amplitude modulator (AM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Amplitude_modulation"/>
    public IOscillator? AmplitudeModulator { get; set; }

    /// <summary>The last sample generated.</summary>
    public double Current { get; private set; }

    private double _frequency;
    /// <summary>The audio frequency in Hertz, i.e. the pitch, of the waveform. This is also the quotient of cycles/second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Audio_frequency"/>
    public double Frequency { get => _frequency; set => SetState(value, _sampleRate); }

    private double _frequencyModulation;
    /// <summary>The amount [0, 1] of output from the frequency modulator to apply.</summary>
    public double FrequencyModulation { get => _frequencyModulation; set => _frequencyModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The frequency modulator (FM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Frequency_modulation"/>
    public IOscillator? FrequencyModulator { get; set; }

    /// <summary>The wave generator used to produce the waveform of the oscillator.</summary>
    public IWaveGenerator? Generator { get; set; }

    /// <summary>Indicates whether the sample polarity should be inverted.</summary>
    public bool InvertPolarity { get; set; }

    /// <summary>The normalized frequency is also known as samples per cycle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    public double NormalizedFrequency { get; private set; }

    private double _offset;
    /// <summary>Initial offset of the oscillator phase.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    public double Offset { get => _offset; set => _offset = Maths.Wrap(value, -1.0, 1.0); }

    private double _phase;
    /// <summary>The position of a point in time (an instant) on the waveform cycle in the range [0, 1].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    public double Phase { get => _phase; set => _phase = Maths.Wrap(value, 0.0, 1.0); }

    private double _phaseModulation;
    /// <summary>The amount [0, 1] of output from the phase modulator to apply.</summary>
    public double PhaseModulation { get => _phaseModulation; set => _phaseModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The pulse modulator (PM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Phase_modulation"/>
    public Oscillator? PhaseModulator { get; set; }

    /// <summary>Audio processors applied before AM, RM, FM and PM.</summary>
    public System.Collections.Generic.List<IAudioProcessorMono> PreProcessors { get; }

    /// <summary>Audio processors applied after AM, RM, FM and PM.</summary>
    public System.Collections.Generic.List<IAudioProcessorMono> PostProcessors { get; }

    /// <summary>Indicates whether the direction of the phase should be reversed.</summary>
    public bool ReversePhase { get; set; }

    private double _ringModulation;
    /// <summary>The amount [0, 1] of output from the ring modulator to apply.</summary>
    public double RingModulation { get => _ringModulation; set => _ringModulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The ring modulator (RM) for the oscillator.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ring_modulation"/>
    public IOscillator? RingModulator { get; set; }

    /// <summary>The period of a sample, in seconds. Can be used to set the sample rate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sampling_(signal_processing)#Theory"/>
    public double SamplePeriod { get; private set; }

    private double _sampleRate;
    /// <summary>The sample rate, i.e. the resolution, of the waveform in hertz.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sampling_(signal_processing)#Sampling_rate"/>
    public double SampleRate { get => _sampleRate; set => SetState(_frequency, value); }

    /// <summary>The number of samples per cycle (in the period) of the waveform frequency.</summary>
    public double SamplesPerCycle { get; private set; }

    /// <summary>The period of the signal, in seconds. Can be used to set the frequency.</summary>
    public double SignalPeriod { get; private set; }

    public Oscillator(IWaveGenerator generator, double frequency, double sampleRate = 44100)
    {
      Generator = generator;

      PreProcessors = new System.Collections.Generic.List<IAudioProcessorMono>();

      PostProcessors = new System.Collections.Generic.List<IAudioProcessorMono>();

      Reset(false);

      SetState(frequency, sampleRate);
    }
    public Oscillator() : this(new WaveGenerator.SineWave(), 8.175798915643707, 44100) { }

    /// <summary>Generates the next sample for the oscillator (all operational components are integrated in ths process).</summary>
    public double Next(double? normalizedFrequency)
    {
      Current = Generator?.GenerateWave(_phase).FrontCenter ?? 0;

      if (InvertPolarity)
      {
        Current = -Current;
      }

      foreach (var processor in PreProcessors)
      {
        Current = processor.ProcessAudio(new MonoSample(Current)).FrontCenter;
      }

      if (AmplitudeModulator != null && _amplitudeModulation > Maths.EpsilonCpp32)
      {
        Current *= AmplitudeModulator.NextSample().FrontCenter * _amplitudeModulation + 1;

        Current /= _amplitudeModulation + 1; // Reset the amplitude after AM applied.
      }

      if (RingModulator != null && _ringModulation > Maths.EpsilonCpp32)
      {
        Current *= RingModulator.NextSample().FrontCenter * _ringModulation;
      }

      foreach (var processor in PostProcessors)
      {
        Current = processor.ProcessAudio(new MonoSample(Current)).FrontCenter;
      }

      if (!normalizedFrequency.HasValue)
      {
        normalizedFrequency = NormalizedFrequency;
      }

      var phaseShift = normalizedFrequency.Value; // Normal phase shift for the current frequency.

      if (PhaseModulator != null && _phaseModulation > Maths.EpsilonCpp32)
      {
        phaseShift += 0.1 * PhaseModulator.Next(normalizedFrequency.Value) * _phaseModulation;
      }

      if (FrequencyModulator != null && _frequencyModulation > Maths.EpsilonCpp32)
      {
        phaseShift += normalizedFrequency.Value * FrequencyModulator.NextSample().FrontCenter * _frequencyModulation;
      }

      if (ReversePhase)
      {
        phaseShift = -phaseShift;
      }

      _phase = Maths.Wrap(_phase + phaseShift, 0, 1); // Ensure phase wraps within cycle.

      System.Diagnostics.Debug.Assert(Current >= -1 && Current <= 1);

      return Current;
    }

    public MonoSample NextSample() => new MonoSample(Next(null));

    /// <summary>Returns a sequence of the specified number of samples.</summary>
    public System.Collections.Generic.IEnumerable<double> GetNext(int count)
    {
      while (count-- > 1)
      {
        yield return NextSample().FrontCenter;
      }
    }

    /// <summary>Resets the phase position using the phase offset. Can be used to "sync" the oscillator.</summary>
    public void Reset(bool resetModulators)
    {
      _phase = _offset;

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
      _frequency = frequency;
      _sampleRate = sampleRate;

      NormalizedFrequency = _frequency / _sampleRate;

      SamplesPerCycle = _sampleRate / _frequency;

      SamplePeriod = 1.0 / _sampleRate;
      SignalPeriod = 1.0 / _frequency;
    }
  }
}
