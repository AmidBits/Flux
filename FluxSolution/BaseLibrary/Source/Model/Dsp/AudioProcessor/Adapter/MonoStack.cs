namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoStack
    : IAudioProcessorMono
  {
    public System.Collections.Generic.List<IAudioProcessorMono> Processors { get; } = new System.Collections.Generic.List<IAudioProcessorMono>();

    public MonoSample ProcessAudio(MonoSample sample)
    {
      foreach (var processor in Processors)
      {
        sample = processor.ProcessAudio(sample);
      }

      return sample;
    }
  }
}
