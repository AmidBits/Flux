namespace Flux.Dsp
{
  /// <summary>A bipolar stereo floating point sample wave processor in the range [-1.0, 1.0].</summary>
  public interface IStereoWaveProcessable
  {
    /// <summary>Process the stereo sample wave. The range is [-1.0, 1.0].</summary>
    IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> wave);
  }
}
