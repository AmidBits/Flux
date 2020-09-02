namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Add as many effects as desired, and they will be applied evenly across the stereo spectrum.</summary>
  public class AutoSpreader
    : IWaveProcessorStereo
  {
    private double _mix, m_dryMix, m_wetMix;
    /// <summary>The balance of dry (source audio) and wet (delay/feedback line) mix.</summary>
    public double DryWetMix
    {
      get => _mix;
      set
      {
        _mix = Maths.Clamp(value, -1.0, 1.0);

        if (_mix > Flux.Maths.EpsilonCpp32)
        {
          m_wetMix = 0.5 * (1.0 + _mix);
          m_dryMix = 1.0 - m_wetMix;
        }
        else if (_mix < -Flux.Maths.EpsilonCpp32)
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

    public System.Collections.Generic.List<IWaveProcessorMono> WaveProcessors { get; } = new System.Collections.Generic.List<IWaveProcessorMono>();

    public StereoSample ProcessAudio(StereoSample stereo)
    {
      var left = 0d;
      var right = 0d;

      var mono = Convert.StereoToMono(stereo.FrontLeft, stereo.FrontRight);

      var processorCount = WaveProcessors.Count;

      var gapSize = 2.0 / (processorCount - 1); // Compute a distribution gap size spread evenly across the stereo field.

      for (var i = 0; i < processorCount; i++)
      {
        if (WaveProcessors[i] as IWaveProcessorMono is var waveProcessor && waveProcessor != null)
        {
          var sampleM = waveProcessor.ProcessAudio(mono);

          var (frontLeft, frontRight) = StereoPan.Apply(-1.0 + gapSize * i, sampleM, sampleM);

          left += frontLeft;
          right += frontRight;
        }
      }

      left /= processorCount;
      right /= processorCount;

      return new StereoSample(stereo.FrontLeft * m_dryMix + left * m_wetMix, stereo.FrontRight * m_dryMix + right * m_wetMix);
    }
  }
}
