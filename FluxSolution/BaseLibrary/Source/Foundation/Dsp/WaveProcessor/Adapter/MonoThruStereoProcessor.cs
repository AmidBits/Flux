namespace Flux.Dsp.AudioProcessor.Adapter
{
  public record class MonoThruStereoProcessor
    : IMonoWaveProcessable
  {
    public IStereoWaveProcessable Stereo { get; }

    public MonoThruStereoProcessor(IStereoWaveProcessable stereo)
    {
      Stereo = stereo;
    }

    public double ProcessMonoWave(double sample)
      => Stereo.ProcessStereoWave(new SampleStereo(sample, sample)).ToMono().FrontCenter;
  }
}
