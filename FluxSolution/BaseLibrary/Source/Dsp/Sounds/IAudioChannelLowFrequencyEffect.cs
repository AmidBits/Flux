namespace Flux.Dsp
{
  /// <summary>Audio channel subwoofer (low frequency effect).</summary>
  public interface IAudioChannelLowFrequencyEffect<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Purple;

    TSelf LowFrequency { get; }
  }
}
