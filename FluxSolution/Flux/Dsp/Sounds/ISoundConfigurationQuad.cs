namespace Flux.Dsp
{
  /// <summary>Audio channel quad 4.0.</summary>
  public interface ISoundConfigurationQuad<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
