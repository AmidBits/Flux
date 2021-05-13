namespace Flux.Media.Dsp.AudioProcessor.Adapter
{
  /// <summary>Add as many effects as desired, and use them as one effect.</summary>
  public class StereoStack
    : IWaveProcessorStereo
  {
    public System.Collections.Generic.IReadOnlyList<IWaveProcessorStereo> Processors { get; } = new System.Collections.Generic.List<IWaveProcessorStereo>();

    public SampleStereo ProcessAudio(SampleStereo sample)
    {
      foreach (var processor in Processors)
        sample = processor.ProcessAudio(sample);

      return sample;
    }
  }
}
