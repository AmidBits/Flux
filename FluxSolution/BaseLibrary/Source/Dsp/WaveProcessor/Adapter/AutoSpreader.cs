namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Add as many effects as desired, and they will be applied evenly across the stereo spectrum.</summary>
  public sealed class AutoSpreader
    : IStereoWaveProcessable
  {
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

    public System.Collections.Generic.List<IMonoWaveProcessable> WaveProcessors { get; } = new System.Collections.Generic.List<IMonoWaveProcessable>();

    public SampleStereo ProcessStereoWave(SampleStereo stereo)
    {
      var left = 0d;
      var right = 0d;

      var mono = Convert.StereoToMono(stereo.FrontLeft, stereo.FrontRight);

      var gapSize = 2.0 / (WaveProcessors.Count - 1); // Compute a distribution gap size spread evenly across the stereo field.

      for (var i = 0; i < WaveProcessors.Count; i++)
      {
        if (WaveProcessors[i] is IMonoWaveProcessable processor && processor is not null)
        {
          var sampleM = processor.ProcessMonoWave(mono);

          var (frontLeft, frontRight) = StereoPan.Apply(-1.0 + gapSize * i, sampleM, sampleM);

          left += frontLeft;
          right += frontRight;
        }
      }

      left /= WaveProcessors.Count;
      right /= WaveProcessors.Count;

      return new SampleStereo(stereo.FrontLeft * m_dryMix + left * m_wetMix, stereo.FrontRight * m_dryMix + right * m_wetMix);
    }
  }
}