namespace Flux.Dsp
{
  /// <summary>Audio channel front center, any format.</summary>
  public interface ISoundConfigurationQuadraphonic<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
  }
}
