namespace Flux.Dsp.AudioProcessor
{
  // https://creatingsound.com/2013/06/dsp-audio-programming-series-part-1/
  // https://stackoverflow.com/questions/11793310/how-to-add-echo-effect-on-audio-file-using-objective-c
  /// <summary>Applies a delay with available feedback, gain and dry/wet mix.</summary>
  public record class MonoDelay
    : IMonoWaveProcessable
  {
    private readonly double[] m_buffer;

    private int m_bufferPosition;

    private double m_feedback, m_feedbackNormalizer;
    /// <summary>The amount (in percent) of delayed audio to resend through the delay line. For example, a setting of 20% sends delayed audio at one-fifth of its original volume, creating echoes that gently fade away. A setting of 200% sends delayed audio at double its original volume, creating echoes that quickly grow in intensity.note: When experimenting with extremely high Feedback settings, turn down your system volume.</summary>
    public double Feedback { get => m_feedback; set => m_feedbackNormalizer = 1 + (m_feedback = System.Math.Clamp(value, 0, 1)); }

    private double m_gain, m_gainCompensation;
    /// <summary>The gain amount of delayed audio [0, 1] to send through the output.</summary>
    public double Gain
    {
      get => m_gain;
      set
      {
        m_gain = System.Math.Clamp(value, 0, 1);

        m_gainCompensation = (2 - (1 - m_gain)) / 2;
        m_gainCompensation = (1 - m_gainCompensation) / m_gainCompensation;
      }
    }

    private double _mix, m_dryMix, m_wetMix;
    /// <summary>The balance of dry (source audio) and wet (delay/feedback line) mix.</summary>
    public double DryWetMix
    {
      get => _mix;
      set
      {
        _mix = System.Math.Clamp(value, -1.0, 1.0);

        if (_mix > GenericMath.EpsilonCpp32)
        {
          m_wetMix = 0.5 * (1.0 + _mix);
          m_dryMix = 1.0 - m_wetMix;
        }
        else if (_mix < -GenericMath.EpsilonCpp32)
        {
          m_dryMix = 0.5 * (1.0 - _mix);
          m_wetMix = 1.0 - m_dryMix;
        }
        else
        {
          m_dryMix = 0.5;
          m_wetMix = 0.5;
        }
      }
    }

    private double _time;
    private int m_timeIndex;
    /// <summary>The amount of buffer time [0, 1] (percent) used of the maximum delay time, where 0 means no delay abd 1 means maximum delay.</summary>
    public double Time { get => _time; set => m_timeIndex = (int)(m_buffer.Length * (_time = System.Math.Clamp(value, 0.0, 1.0))); }

    public MonoDelay(int maxDelayTimeInSeconds, double sampleRate)
    {
      m_buffer = new double[(int)System.Math.Ceiling(sampleRate * maxDelayTimeInSeconds)];
      m_bufferPosition = 0;

      Feedback = 0.0;
      Gain = 0.0;
      Time = 1.0;
      DryWetMix = 0.0;
    }
    public MonoDelay(int maxDelayTimeInSeconds)
      : this(maxDelayTimeInSeconds, 44100.0)
    { }
    public MonoDelay()
      : this(1)
    { }

    //public double ProcessAudioMono(double sample)
    //{
    //  var bufferSample = _buffer[_bufferPosition];

    //  _buffer[_bufferPosition] = (sample + _feedback * bufferSample) / _feedbackCompensation;

    //  _bufferPosition = (_bufferPosition + 1) % _timeIndex;

    //  return sample * (_dryMix + _gainCompensation * _wetMix) + (_gain * bufferSample) * _wetMix;
    //}

    public double ProcessMonoWave(double wave)
    {
      var bufferSample = m_buffer[m_bufferPosition] * m_gain;

      m_buffer[m_bufferPosition] = (wave + bufferSample * m_feedback) / m_feedbackNormalizer;

      m_bufferPosition = (m_bufferPosition + 1) % m_timeIndex;

      return ((wave * m_dryMix + bufferSample * m_wetMix) * m_gainCompensation);
    }

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => (WaveMono<double>)ProcessMonoWave(mono.Wave);
  }
}
