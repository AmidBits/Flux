namespace Flux.Dsp
{
  /// <summary>Audio channel back left.</summary>
  public interface IAudioChannelBackLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf BackLeft { get; }
  }
}
