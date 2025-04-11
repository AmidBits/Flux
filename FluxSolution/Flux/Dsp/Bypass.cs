namespace Flux.Dsp
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public record class Bypass
    : WaveFilters.IMonoWaveFilterable, WaveProcessors.IMonoWaveProcessable, WaveProcessors.IStereoWaveProcessable
  {
    public double FilterMonoWave(double wave) => wave;

    // IMonoWaveFilterable
    public Waves.IWaveMono<double> FilterMonoWave(Waves.IWaveMono<double> wave) => wave;

    // IMonoWaveProcessable
    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> wave) => wave;

    // IStereoWaveProcessable
    public Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> wave) => wave;
  }
}
