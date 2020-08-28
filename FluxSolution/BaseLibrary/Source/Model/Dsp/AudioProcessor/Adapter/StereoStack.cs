namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Add as many effects as desired, and use them as one effect.</summary>
  public class StereoStack
    : IAudioProcessorStereo
  {
    public System.Collections.Generic.List<IAudioProcessorStereo> Processors { get; } = new System.Collections.Generic.List<IAudioProcessorStereo>();

    public StereoSample ProcessAudio(StereoSample sample)
    {
      foreach (var processor in Processors)
      {
        sample = processor.ProcessAudio(sample);
      }

      return sample;
    }
  }
}
