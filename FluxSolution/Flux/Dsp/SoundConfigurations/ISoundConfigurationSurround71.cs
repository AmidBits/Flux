namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration surround 7.1.</summary>
  public interface ISoundConfigurationSurround71<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelCenter<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>, IAudioChannelSideLeft<TSelf>, IAudioChannelSideRight<TSelf>
    , IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
