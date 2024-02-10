namespace Flux.Dsp
{
  /// <summary>Audio channel front center, any format.</summary>
  public interface ISoundConfiguration51<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelFrontCenter<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>, IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
