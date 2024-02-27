namespace Flux.Dsp
{
  /// <summary>A bipolar [-1.0, 1.0] quadrophonic floating point sample wave processor.</summary>
  public interface IQuadWaveProcessable
  {
    /// <summary>Process the quad sample wave. The range is [-1.0, 1.0], in and out.</summary>
    IWaveQuad<double> ProcessQuadWave(IWaveQuad<double> sample);
  }
}
