namespace Flux.Dsp.Waves
{
  /// <summary>Stereo (left and right) wave, range [-1, +1].</summary>
  public interface IWaveStereo<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    TSelf SampleLeft { get; }
    TSelf SampleRight { get; }
  }
}
