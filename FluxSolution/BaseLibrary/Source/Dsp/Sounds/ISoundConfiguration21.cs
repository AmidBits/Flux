namespace Flux.Dsp
{
  /// <summary>Audio channel front center, any format.</summary>
  public interface ISoundConfiguration21<TSelf>
    : IAudioChannelFrontLeft<TSelf>, IAudioChannelFrontRight<TSelf>, IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
