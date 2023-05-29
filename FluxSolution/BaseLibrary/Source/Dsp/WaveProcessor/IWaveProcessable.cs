namespace Flux.Dsp
{
  /// <summary>A bipolar mono floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IMonoWaveProcessable
  {
    public static IMonoWaveProcessable Empty => new EmptyMonoWaveProcessor();

    /// <summary>Process the mono sample wave. The range is [-1.0, 1.0].</summary>
    IWaveMono<double> ProcessMonoWave(IWaveMono<double> wave);

    private sealed class EmptyMonoWaveProcessor
      : IMonoWaveProcessable
    {
      public IWaveMono<double> ProcessMonoWave(IWaveMono<double> wave) => throw new System.NotImplementedException(nameof(EmptyMonoWaveProcessor));
    }
  }

  /// <summary>A bipolar stereo floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IStereoWaveProcessable
  {
    public static IStereoWaveProcessable Empty => new EmptyStereoWaveProcessor();

    /// <summary>Process the stereo sample wave. The range is [-1.0, 1.0].</summary>
    IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> wave);

    private sealed class EmptyStereoWaveProcessor
      : IStereoWaveProcessable
    {
      public IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> wave) => throw new System.NotImplementedException(nameof(EmptyStereoWaveProcessor));
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
