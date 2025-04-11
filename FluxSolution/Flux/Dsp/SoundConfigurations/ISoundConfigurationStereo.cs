namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration stereo 2.0.</summary>
  public interface ISoundConfigurationStereo<TSelf>
    : AudioChannels.IAudioChannelFrontLeft<TSelf>
    , AudioChannels.IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
