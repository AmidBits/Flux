namespace Flux.Dsp.WaveProcessor
{
  /// <summary>A bipolar [-1.0, 1.0] mono floating point sample wave processor.</summary>
  public interface IMonoWaveProcessable
  {
    /// <summary>Process the mono sample wave. The range is [-1.0, 1.0], in and out.</summary>
    Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> wave);
  }
}
