namespace Flux.Dsp
{
  /// <summary>Audio channel front right.</summary>
  public interface IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf FrontRight { get; }
  }
}
