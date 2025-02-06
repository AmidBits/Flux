namespace Flux.Dsp.Waves
{
  /// <summary>Quad (back left, back right, front left and front right) wave, range [-1, +1].</summary>
  public interface IWaveQuad<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    TSelf WaveBackLeft { get; }
    TSelf WaveBackRight { get; }
    TSelf WaveFrontLeft { get; }
    TSelf WaveFrontRight { get; }
  }
}
