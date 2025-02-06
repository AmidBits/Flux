namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration front 5.1.</summary>
  public interface ISoundConfigurationFront51<TSelf>
    : IAudioChannelBackLeft<TSelf>, IAudioChannelBackRight<TSelf>, IAudioChannelCenter<TSelf>, IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
    , IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
