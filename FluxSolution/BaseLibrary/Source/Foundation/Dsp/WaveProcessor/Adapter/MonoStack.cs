namespace Flux.Dsp.AudioProcessor.Adapter
{
  public sealed class MonoStack
    : IMonoWaveProcessable
  {
    public System.Collections.Generic.IReadOnlyList<IMonoWaveProcessable> Processors { get; } = new System.Collections.Generic.List<IMonoWaveProcessable>();

    public double ProcessMonoWave(double sample)
    {
      foreach (var processor in Processors)
        sample = processor.ProcessMonoWave(sample);

      return sample;
    }
  }
}
