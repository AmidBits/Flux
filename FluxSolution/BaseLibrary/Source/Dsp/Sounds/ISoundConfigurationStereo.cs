namespace Flux.Dsp
{
  /// <summary>Stereo sample, left and right, any format.</summary>
  public interface ISoundConfigurationStereo<TSelf>
    : IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
