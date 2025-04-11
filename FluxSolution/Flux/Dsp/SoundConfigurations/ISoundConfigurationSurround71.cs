namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration surround 7.1.</summary>
  public interface ISoundConfigurationSurround71<TSelf>
    : AudioChannels.IAudioChannelBackLeft<TSelf>
    , AudioChannels.IAudioChannelBackRight<TSelf>
    , AudioChannels.IAudioChannelCenter<TSelf>
    , AudioChannels.IAudioChannelFrontLeft<TSelf>
    , AudioChannels.IAudioChannelFrontRight<TSelf>
    , AudioChannels.IAudioChannelSideLeft<TSelf>
    , AudioChannels.IAudioChannelSideRight<TSelf>
    , AudioChannels.IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
