namespace Flux.Dsp.AudioProcessor.Adapter
{
  public sealed class MonoThruStereoProcessor
    : IMonoWaveProcessable
  {
    public IStereoWaveProcessable Stereo { get; }

    public MonoThruStereoProcessor(IStereoWaveProcessable stereo)
    {
      Stereo = stereo;
    }

    public double ProcessMonoWave(double sample)
      => Stereo.ProcessStereoWave(new SampleStereo(sample)).ToMono().FrontCenter;
  }
}
