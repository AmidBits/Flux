namespace Flux.Dsp.WaveProcessors
{
  /// <summary>A bipolar [-1.0, 1.0] quadrophonic floating point sample wave processor.</summary>
  public interface IQuadWaveProcessable
  {
    /// <summary>Process the quad sample wave. The range is [-1.0, 1.0], in and out.</summary>
    Waves.IWaveQuad<double> ProcessQuadWave(Waves.IWaveQuad<double> sample);
  }
}
