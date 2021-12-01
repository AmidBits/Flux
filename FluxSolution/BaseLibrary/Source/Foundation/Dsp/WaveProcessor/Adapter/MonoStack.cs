namespace Flux.Dsp.AudioProcessor.Adapter
{
#if NET5_0
  public sealed class MonoStack
#else
  public record struct MonoStack
#endif
    : IWaveProcessorMono
  {
    public System.Collections.Generic.IReadOnlyList<IWaveProcessorMono> Processors { get; } = new System.Collections.Generic.List<IWaveProcessorMono>();

    public double ProcessAudio(double sample)
    {
      foreach (var processor in Processors)
        sample = processor.ProcessAudio(sample);

      return sample;
    }
  }
}
