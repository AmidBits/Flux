namespace Flux.Dsp
{
  /// <summary>Audio channel back right.</summary>
  public interface IAudioChannelBackRight<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    TSelf BackRight { get; }
  }
}
