namespace Flux.Dsp
{
  /// <summary>A bipolar [-1, 1] sample audio processor in mono.</summary>
  public interface IAudioProcessorMono
  {
    static IAudioProcessorMono Empty => EmptyAudioProcessorMono.Instance;

    /// <returns>Process the shaped sample in the [-1, 1] range in mono.</returns>
    //double ProcessAudioMono(double sample);
    MonoSample ProcessAudio(MonoSample sample);

    private class EmptyAudioProcessorMono
      : IAudioProcessorMono
    {
      public static IAudioProcessorMono Instance = new EmptyAudioProcessorMono();

      public MonoSample ProcessAudio(MonoSample sample)
        => throw new System.NotImplementedException(nameof(EmptyAudioProcessorMono));
    }
  }

  /// <summary>A bipolar [-1, 1] sample audio processor in stereo.</summary>
  public interface IAudioProcessorStereo
  {
    /// <returns>Process the shaped samples in the [-1, 1] range in stereo.</returns>
    //(double left, double right) ProcessAudioStereo(double left, double right);
    StereoSample ProcessAudio(StereoSample sample);

    private class EmptyAudioProcessorStereo
      : IAudioProcessorStereo
    {
      public static IAudioProcessorStereo Instance = new EmptyAudioProcessorStereo();

      public StereoSample ProcessAudio(StereoSample sample)
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
