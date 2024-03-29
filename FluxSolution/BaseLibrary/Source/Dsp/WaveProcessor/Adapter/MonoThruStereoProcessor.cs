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

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> wave)
      => Stereo.ProcessStereoWave(wave.ToStereoWave()).ToMonoWave();
  }
}
