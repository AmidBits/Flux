namespace Flux.Dsp
{
  /// <summary>Audio channel front left.</summary>
  public interface IAudioChannelFrontLeft<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    TSelf FrontLeft { get; }
  }
}
