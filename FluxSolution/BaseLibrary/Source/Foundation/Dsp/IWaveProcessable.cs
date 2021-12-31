namespace Flux.Dsp
{
  /// <summary>A bipolar mono floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IMonoWaveProcessable
  {
    static IMonoWaveProcessable Empty
      => new EmptyMonoWaveProcessor();

    /// <summary>Process the mono sample wave. The range is [-1.0, 1.0].</summary>
    double ProcessMonoWave(double sample);

    private sealed class EmptyMonoWaveProcessor
      : IMonoWaveProcessable
    {
      public double ProcessMonoWave(double sample)
        => throw new System.NotImplementedException(nameof(EmptyMonoWaveProcessor));
    }
  }

  /// <summary>A bipolar stereo floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IStereoWaveProcessable
  {
    static IStereoWaveProcessable Empty
      => new EmptyStereoWaveProcessor();

    /// <summary>Process the stereo sample wave. The range is [-1.0, 1.0].</summary>
    SampleStereo ProcessStereoWave(SampleStereo sample);

    private sealed class EmptyStereoWaveProcessor
      : IStereoWaveProcessable
    {
      public SampleStereo ProcessStereoWave(SampleStereo sample)
        => throw new System.NotImplementedException(nameof(EmptyStereoWaveProcessor));
    }
  }

  /// <summary>A bipolar [-1, 1] sample audio processor in quadrophonic.</summary>
  //public interface IAudioProcessorQuad
  //{
  //  /// <returns>Process the shaped samples in the [-1, 1] range in quad.</returns>
  //  ISampleQuad ProcessAudio(ISampleQuad sample);

  //  private class EmptyAudioProcessorQuad
  //    : IAudioProcessorQuad
  //  {
  //    public static IAudioProcessorQuad Instance = new EmptyAudioProcessorQuad();

  //    public ISampleQuad ProcessAudio(ISampleQuad sample)
  //      => throw new System.NotImplementedException(nameof(EmptyAudioProcessorQuad));
  //  }
  //}
}
