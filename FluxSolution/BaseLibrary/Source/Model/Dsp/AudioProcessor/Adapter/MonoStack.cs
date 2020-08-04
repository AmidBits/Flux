namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoStack : IAudioProcessorMono
  {
    public System.Collections.Generic.List<IAudioProcessorMono> Processors { get; } = new System.Collections.Generic.List<IAudioProcessorMono>();

    public ISampleMono ProcessAudio(ISampleMono sample)
    {
      foreach (var processor in Processors)
      {
        sample = processor.ProcessAudio(sample);
      }

      return sample;
    }
  }
}
