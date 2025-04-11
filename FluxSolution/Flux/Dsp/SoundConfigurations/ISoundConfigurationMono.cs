namespace Flux.Dsp.SoundConfigurations
{
  /// <summary>Sound configuration mono 1.0.</summary>
  public interface ISoundConfigurationMono<TSelf>
    : AudioChannels.IAudioChannelCenter<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
