namespace Flux.Dsp
{
  /// <summary>Sound configuration atmos 7.1.4.</summary>
  public interface ISoundConfigurationAtmos714<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelCenter<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>, IAudioChannelSideLeft<TSelf>, IAudioChannelSideRight<TSelf>
    , IAudioChannelLowFrequencyEffect<TSelf>
    , IAudioChannelHeightBackLeft<TSelf>, IAudioChannelHeightBackRight<TSelf>, IAudioChannelHeightFrontLeft<TSelf>, IAudioChannelHeightFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
