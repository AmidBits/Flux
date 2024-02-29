namespace Flux.Dsp
{
  /// <summary>Sound configuration mono 1.0.</summary>
  public interface ISoundConfigurationMono<TSelf>
    : IAudioChannelCenter<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
