namespace Flux.Dsp.AudioProcessor
{
  /// <summary>A basic limiter.</summary>
  /// <see cref="https://github.com/markheath/skypevoicechanger/blob/master/SkypeVoiceChanger/Effects/EventHorizon.cs"/>
  public record class MonoLimiter
    : IMonoWaveProcessable
  {
    private double m_threshold; // Defaults to zero.
    /// <summary>Threshold in the range [-30, 0]. Default is 0.</summary>
    public double Threshold
    {
      get => m_threshold;
      set
      {
        m_threshold = System.Math.Clamp(value, -30, 0);

        SetState();
      }
    }

    private double m_ceiling = -0.1;
    /// <summary>Ceiling in the range [-20, 0]. Default is -0.1.</summary>
    public double Ceiling
    {
      get => m_ceiling;
      set
      {
        m_ceiling = System.Math.Clamp(value, -20, 0);

        SetState();
      }
    }

    private double m_softClipDecibel = 2;
    /// <summary>Threshold in the range [0, 6]. Default is 2.</summary>
    public double SoftClipDecibel
    {
      get => m_softClipDecibel;
      set
      {
        m_softClipDecibel = System.Math.Clamp(value, 0, 6);

        SetState();
      }
    }

    private double m_softClipRatio = 10;
    /// <summary>Threshold in the range [3, 20]. Default is 10.</summary>
    public double SoftClipRatio
    {
      get => m_softClipRatio;
      set
      {
        m_softClipRatio = System.Math.Clamp(System.Math.Round(value), 3, 20);

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

    public double ProcessMonoWave(double wave)
    {
      // var peak = System.Math.Abs(wave);

      wave *= m_computedMakeup;

      var abs = System.Math.Abs(wave);
      var sign = System.Math.Sign(wave);

      var overdB = 2.08136898 * System.Math.Log(abs) * Convert.Logarithm2Decibel - m_computedCeilingDecibel;

      if (abs > m_computedSoftClipV)
        wave = sign * (m_computedSoftClipV + System.Math.Exp(overdB * m_computedSoftClipMult) * Convert.Decibel2Logarithm);

      return System.Math.Min(m_computedCeiling, System.Math.Abs(wave)) * System.Math.Sign(wave);
    }

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => (WaveMono<double>)ProcessMonoWave(mono.Wave);
  }
}
