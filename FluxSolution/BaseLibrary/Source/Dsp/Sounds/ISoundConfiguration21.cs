namespace Flux.Dsp
{
  /// <summary>Audio channel front center, any format.</summary>
  public interface ISoundConfiguration21<TSelf>
    : IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>, IAudioChannelLowFrequencyEffect<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
  }
}
