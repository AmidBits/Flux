namespace Flux.Dsp.WaveProcessor.Adapter
{
  public record class MonoStack
    : IMonoWaveProcessable
  {
    public System.Collections.Generic.IReadOnlyList<IMonoWaveProcessable> Processors { get; } = new System.Collections.Generic.List<IMonoWaveProcessable>();

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> sample)
    {
      foreach (var processor in Processors)
        sample = processor.ProcessMonoWave(sample);

      return sample;
    }
  }
}
