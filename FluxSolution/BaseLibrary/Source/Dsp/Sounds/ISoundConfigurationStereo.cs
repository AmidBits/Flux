namespace Flux.Dsp
{
  /// <summary>Stereo sample, left and right, any format.</summary>
  public interface ISoundConfigurationStereo<TSelf>
    : IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
  }
}
