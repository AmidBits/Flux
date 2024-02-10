namespace Flux.Dsp.WaveFilter
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public record class Bypass
    : IMonoWaveFilterable, IMonoWaveProcessable, IStereoWaveProcessable
  {
    public double FilterMonoWave(double wave) => wave;

    // IMonoWaveFilterable
    public IWaveMono<double> FilterMonoWave(IWaveMono<double> wave) => wave;

    // IMonoWaveProcessable
    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> wave) => wave;

    // IStereoWaveProcessable
    public IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> wave) => wave;
  }
}
