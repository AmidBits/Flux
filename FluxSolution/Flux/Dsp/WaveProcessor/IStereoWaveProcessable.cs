namespace Flux.Dsp.WaveProcessor
{
  /// <summary>A bipolar [-1.0, 1.0] stereo floating point sample wave processor.</summary>
  public interface IStereoWaveProcessable
  {
    /// <summary>Process the stereo sample wave. The range is [-1.0, 1.0], in and out.</summary>
    Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> wave);
  }
}
