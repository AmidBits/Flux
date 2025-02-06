namespace Flux.Dsp.Waves
{
  /// <summary>Mono wave, range [-1, +1].</summary>
  public interface IWaveMono<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    TSelf Wave { get; }
  }
}
