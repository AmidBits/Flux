namespace Flux.Dsp.AudioProcessor
{
  // https://creatingsound.com/2013/06/dsp-audio-programming-series-part-1/
  // https://stackoverflow.com/questions/11793310/how-to-add-echo-effect-on-audio-file-using-objective-c
  public class DelayMono : IAudioProcessorMono
  {
    private readonly double[] m_buffer;

    private int m_bufferPosition;

    private double m_feedback, m_feedbackCompensation;
    /// <summary>The amount of delayed audio to resend through the delay line. For example, a setting of 20% sends delayed audio at one-fifth of its original volume, creating echoes that gently fade away. A setting of 200% sends delayed audio at double its original volume, creating echoes that quickly grow in intensity.note: When experimenting with extremely high Feedback settings, turn down your system volume.</summary>
    public double Feedback { get => m_feedback; set => m_feedbackCompensation = 1.0 + (m_feedback = Math.Clamp(value, 0.0, 1.0)); }

    private double m_gain, m_gainCompensation;
    /// <summary>The gain amount of delayed audio [0, 1] to send through the output.</summary>
    public double Gain { get => m_gain; set => m_gainCompensation = 1.0 - (m_gain = Math.Clamp(value, 0.0, 1.0)); }

    private double _time;
    private int m_timeIndex;
    /// <summary>The amount of buffer time [0, 1] (percent) used of the delay time, where 0 means no delay abd 1 means maximum delay.</summary>
    public double Time { get => _time; set => m_timeIndex = (int)(m_buffer.Length * (_time = Math.Clamp(value, 0.0, 1.0))); }

    private double _mix, m_dryMix, m_wetMix;
    /// <summary>The balance of dry (source audio) and wet (delay/feedback line) mix.</summary>
    public double Mix
    {
      get => _mix;
      set
      {
        _mix = Math.Clamp(value, -1.0, 1.0);

        if (_mix > Flux.Math.EpsilonCpp32)
        {
          m_dryMix = 1.0 - (m_wetMix = 0.5 * (1.0 + _mix));
        }
        else if (_mix < -Flux.Math.EpsilonCpp32)
        {
          m_wetMix = 1.0 - (m_dryMix = 0.5 * (1.0 - _mix));
        }
        else
        {
          m_dryMix = 0.5;
          m_wetMix = 0.5;
        }
      }
    }

    public DelayMono(int maxDelayTimeInSeconds, double sampleRate)
    {
      m_buffer = new double[(int)System.Math.Round(sampleRate * maxDelayTimeInSeconds)];
      m_bufferPosition = 0;

      Feedback = 0.0;
      Gain = 0.0;
      Time = 1.0;
      Mix = 0.0;
    }
    public DelayMono(int maxDelayTimeInSeconds) : this(maxDelayTimeInSeconds, 44100.0) { }
    public DelayMono() : this(1) { }

    //public double ProcessAudioMono(double sample)
    //{
    //  var bufferSample = _buffer[_bufferPosition];

    //  _buffer[_bufferPosition] = (sample + _feedback * bufferSample) / _feedbackCompensation;

    //  _bufferPosition = (_bufferPosition + 1) % _timeIndex;

    //  return sample * (_dryMix + _gainCompensation * _wetMix) + (_gain * bufferSample) * _wetMix;
    //}

    public ISampleMono ProcessAudio(ISampleMono sample)
    {
      var bufferSample = m_buffer[m_bufferPosition];

      m_buffer[m_bufferPosition] = (sample.FrontCenter + m_feedback * bufferSample) / m_feedbackCompensation;

      m_bufferPosition = (m_bufferPosition + 1) % m_timeIndex;

      return new MonoSample(sample.FrontCenter * (m_dryMix + m_gainCompensation * m_wetMix) + (m_gain * bufferSample) * m_wetMix);
    }
  }

  public class DelayStereo : IAudioProcessorStereo
  {
    public DelayMono Left { get; }
    public DelayMono Right { get; }

    public double Feedback { get => Left.Feedback; set => Right.Feedback = Left.Feedback = Math.Clamp(value, 0.0, 1.0); }

    public double Gain { get => Left.Gain; set => Right.Gain = Left.Gain = Math.Clamp(value, 0.0, 1.0); }

    public double Time { get => Left.Time; set => Right.Time = Left.Time = Math.Clamp(value, 0.0, 1.0); }

    public double Mix { get => Left.Mix; set => Right.Mix = Left.Mix = Math.Clamp(value, -1.0, 1.0); }

    public DelayStereo(int maxDelayTimeInSeconds, double sampleRate)
    {
      Left = new DelayMono(maxDelayTimeInSeconds, sampleRate);
      Right = new DelayMono(maxDelayTimeInSeconds, sampleRate);
    }
    public DelayStereo(int maxDelayTimeInSeconds) : this(maxDelayTimeInSeconds, 44100.0) { }
    public DelayStereo() : this(1) { }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
