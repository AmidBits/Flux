namespace Flux.Dsp.WaveProcessors.Adapter
{
  /// <summary>Add as many effects as desired, and use them as one effect.</summary>
  public record class StereoStack
    : IStereoWaveProcessable
  {
    public System.Collections.Generic.IReadOnlyList<IStereoWaveProcessable> Processors { get; } = new System.Collections.Generic.List<IStereoWaveProcessable>();

    public Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> sample)
    {
      foreach (var processor in Processors)
        sample = processor.ProcessStereoWave(sample);

      return sample;
    }
  }
}
