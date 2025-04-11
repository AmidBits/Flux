namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration front 5.1.</summary>
  public interface ISoundConfigurationFront51<TSelf>
    : AudioChannels.IAudioChannelBackLeft<TSelf>
    , AudioChannels.IAudioChannelBackRight<TSelf>
    , AudioChannels.IAudioChannelCenter<TSelf>
    , AudioChannels.IAudioChannelFrontLeft<TSelf>
    , AudioChannels.IAudioChannelFrontRight<TSelf>
    , AudioChannels.IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
