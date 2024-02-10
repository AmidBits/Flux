namespace Flux.Dsp
{
  /// <summary>Audio channel front center, any format.</summary>
  public interface ISoundConfigurationQuadraphonic<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
