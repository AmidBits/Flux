namespace Flux.Dsp
{
  /// <summary>Mono sample, any format.</summary>
  public interface ISoundConfigurationMono<TSelf>
    : IAudioChannelFrontCenter<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
