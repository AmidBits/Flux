namespace Flux.Dsp
{
  /// <summary>A bipolar mono floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IMonoWaveProcessable
  {
    /// <summary>Process the mono sample wave. The range is [-1.0, 1.0].</summary>
    IWaveMono<double> ProcessMonoWave(IWaveMono<double> wave);
  }
}
