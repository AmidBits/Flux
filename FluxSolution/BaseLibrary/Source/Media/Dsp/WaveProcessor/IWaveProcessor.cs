namespace Flux.Media.Dsp
{
  /// <summary>A bipolar [-1, 1] sample audio processor in mono.</summary>
  public interface IWaveProcessorMono
  {
    static IWaveProcessorMono Empty => EmptyAudioProcessorMono.Instance;

    /// <returns>Process the shaped sample in the [-1, 1] range in mono.</returns>
    //double ProcessAudioMono(double sample);
    double ProcessAudio(double sample);

    private class EmptyAudioProcessorMono
      : IWaveProcessorMono
    {
      public static IWaveProcessorMono Instance = new EmptyAudioProcessorMono();

      public double ProcessAudio(double sample)
        => throw new System.NotImplementedException(nameof(EmptyAudioProcessorMono));
    }
  }

  /// <summary>A bipolar [-1, 1] sample audio processor in stereo.</summary>
  public interface IWaveProcessorStereo
  {
    /// <returns>Process the shaped samples in the [-1, 1] range in stereo.</returns>
    //(double left, double right) ProcessAudioStereo(double left, double right);
    SampleStereo ProcessAudio(SampleStereo sample);

    private class EmptyAudioProcessorStereo
      : IWaveProcessorStereo
    {
      public static IWaveProcessorStereo Instance = new EmptyAudioProcessorStereo();

      public SampleStereo ProcessAudio(SampleStereo sample)
        => throw new System.NotImplementedException(nameof(EmptyAudioProcessorStereo));
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
