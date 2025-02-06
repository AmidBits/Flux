namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration atmos 5.1.4.</summary>
  public interface ISoundConfigurationAtmos514<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelCenter<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
    , IAudioChannelLowFrequencyEffect<TSelf>
    , IAudioChannelHeightBackLeft<TSelf>, IAudioChannelHeightBackRight<TSelf>, IAudioChannelHeightFrontLeft<TSelf>, IAudioChannelHeightFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
