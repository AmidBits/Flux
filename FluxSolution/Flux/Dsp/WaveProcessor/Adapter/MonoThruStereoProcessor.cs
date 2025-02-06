namespace Flux.Dsp.WaveProcessor.Adapter
{
  public record class MonoThruStereoProcessor
    : IMonoWaveProcessable
  {
    public IStereoWaveProcessable Stereo { get; }

    public MonoThruStereoProcessor(IStereoWaveProcessable stereo)
    {
      Stereo = stereo;
    }

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> wave)
      => Stereo.ProcessStereoWave(wave.ToStereoWave()).ToMonoWave();
  }
}
