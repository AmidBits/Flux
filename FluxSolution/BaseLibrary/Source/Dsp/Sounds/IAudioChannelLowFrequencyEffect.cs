namespace Flux.Dsp
{
  /// <summary>Audio channel low frequency effect (sub-woofer).</summary>
  public interface IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf LowFrequency { get; }
  }
}
