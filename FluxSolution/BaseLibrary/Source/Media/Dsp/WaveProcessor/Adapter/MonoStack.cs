namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoStack
    : IWaveProcessorMono
  {
    public System.Collections.Generic.IReadOnlyList<IWaveProcessorMono> Processors { get; } = new System.Collections.Generic.List<IWaveProcessorMono>();

    public double ProcessAudio(double sample)
    {
      foreach (var processor in Processors)
      {
        sample = processor.ProcessAudio(sample);
      }

      return sample;
    }
  }
}
