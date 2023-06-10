namespace Flux.Dsp
{
  /// <summary>Mono sample, any format.</summary>
  public interface ISoundConfigurationMono<TSelf>
    : IAudioChannelFrontCenter<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
  }
}
