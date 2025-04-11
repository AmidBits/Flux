namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Audio channel quad 4.0.</summary>
  public interface ISoundConfigurationQuad<TSelf>
    : AudioChannels.IAudioChannelBackLeft<TSelf>
    , AudioChannels.IAudioChannelBackRight<TSelf>
    , AudioChannels.IAudioChannelFrontLeft<TSelf>
    , AudioChannels.IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
