namespace Flux.Dsp
{
  /// <summary>Audio channel front center.</summary>
  public interface IAudioChannelFrontCenter<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf FrontCenter { get; }
  }
}
