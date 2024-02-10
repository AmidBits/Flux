namespace Flux.Dsp
{
  /// <summary>Audio channel front left.</summary>
  public interface IAudioChannelFrontLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf FrontLeft { get; }
  }
}
