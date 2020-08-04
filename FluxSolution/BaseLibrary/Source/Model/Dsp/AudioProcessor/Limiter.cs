namespace Flux.Dsp.AudioProcessor
{
  /// <see cref="https://github.com/markheath/skypevoicechanger/blob/master/SkypeVoiceChanger/Effects/EventHorizon.cs"/>
  public class LimiterMono : IAudioProcessorMono
  {
    private double m_threshold;
    public double Threshold
    {
      get => m_threshold;
      set
      {
        m_threshold = Math.Clamp(value, -30.0, 0.0);

        SetState();
      }
    }

    private double m_ceiling;
    public double Ceiling
    {
      get => m_ceiling;
      set
      {
        m_ceiling = Math.Clamp(value, -20.0, 0.0);

        SetState();
      }
    }

    private double m_softClipDecibel;
    public double SoftClipDecibel
    {
      get => m_softClipDecibel;
      set
      {
        m_softClipDecibel = Math.Clamp(value, 0.0, 6.0);

        SetState();
      }
    }

    private double m_softClipRatio;
    public double SoftClipRatio
    {
      get => m_softClipRatio;
      set
      {
        m_softClipRatio = Math.Clamp(System.Math.Round(value), 3.0, 20.0);

        SetState();
      }
    }

    //private double _computedThreshold;
    private double m_computedThresholdDecibel;
    private double m_computedCeiling;
    private double m_computedCeilingDecibel;
    private double m_computedMakeup;
    //private double _computedMakeupDecibel;
    private double m_computedSoftClip;
    private double m_computedSoftClipV;
    //private double _computedSoftClipComp;
    private double m_computedPeakDecibel;
    //private double _computedPeakLevel;
    //private double _computedSoftClipRatio;
    private double m_computedSoftClipMult;

    private void SetState()
    {
      //_computedThreshold = System.Math.Exp(_threshold * Convert.Decibel2Logarithm);
      m_computedThresholdDecibel = m_threshold;
      m_computedCeiling = System.Math.Exp(m_ceiling * Convert.Decibel2Logarithm);
      m_computedCeilingDecibel = m_ceiling;
      m_computedMakeup = System.Math.Exp((m_computedCeilingDecibel - m_computedThresholdDecibel) * Convert.Decibel2Logarithm);
      //_computedMakeupDecibel = _computedCeilingDecibel - _computedThresholdDecibel;
      m_computedSoftClip = -m_softClipDecibel;
      m_computedSoftClipV = System.Math.Exp(m_computedSoftClip * Convert.Decibel2Logarithm);
      //_computedSoftClipComp = System.Math.Exp(-_computedSoftClip * Convert.Decibel2Logarithm);
      m_computedPeakDecibel = m_computedCeilingDecibel + 25;
      //_computedPeakLevel = System.Math.Exp(_computedPeakDecibel * Convert.Decibel2Logarithm);
      //_computedSoftClipRatio = _softClipRatio;
      m_computedSoftClipMult = System.Math.Abs((m_computedCeilingDecibel - m_computedSoftClip) / (m_computedPeakDecibel - m_computedSoftClip));
    }

    public ISampleMono ProcessAudio(ISampleMono mono)
    {
      // var peak = System.Math.Abs(sample);

      var sample = mono.FrontCenter;

      sample *= m_computedMakeup;

      var abs = System.Math.Abs(sample);
      var sign = System.Math.Sign(sample);

      var overdB = 2.08136898 * System.Math.Log(abs) * Convert.Logarithm2Decibel - m_computedCeilingDecibel;

      if (abs > m_computedSoftClipV)
      {
        sample = sign * (m_computedSoftClipV + System.Math.Exp(overdB * m_computedSoftClipMult) * Convert.Decibel2Logarithm);
      }

      return new MonoSample(System.Math.Min(m_computedCeiling, System.Math.Abs(sample)) * sign);
    }
  }

  public class LimiterStereo : IAudioProcessorStereo
  {
    public LimiterMono Left { get; }
    public LimiterMono Right { get; }

    public LimiterStereo()
    {
      Left = new LimiterMono();
      Right = new LimiterMono();
    }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
