namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration atmos 5.1.4.</summary>
  public interface ISoundConfigurationAtmos514<TSelf>
    : AudioChannels.IAudioChannelBackLeft<TSelf>
    , AudioChannels.IAudioChannelBackRight<TSelf>
    , AudioChannels.IAudioChannelCenter<TSelf>
    , AudioChannels.IAudioChannelFrontLeft<TSelf>
    , AudioChannels.IAudioChannelFrontRight<TSelf>
    , AudioChannels.IAudioChannelLowFrequencyEffect<TSelf>
    , AudioChannels.IAudioChannelHeightBackLeft<TSelf>
    , AudioChannels.IAudioChannelHeightBackRight<TSelf>
    , AudioChannels.IAudioChannelHeightFrontLeft<TSelf>
    , AudioChannels.IAudioChannelHeightFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
