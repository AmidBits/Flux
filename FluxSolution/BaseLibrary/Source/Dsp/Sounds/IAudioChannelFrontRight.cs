namespace Flux.Dsp
{
  /// <summary>Audio channel front right.</summary>
  public interface IAudioChannelFrontRight<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    TSelf FrontRight { get; }
  }
}
