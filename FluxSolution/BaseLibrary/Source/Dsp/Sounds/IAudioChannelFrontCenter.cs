namespace Flux.Dsp
{
  /// <summary>Audio channel front center.</summary>
  public interface IAudioChannelFrontCenter<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    TSelf FrontCenter { get; }
  }
}
