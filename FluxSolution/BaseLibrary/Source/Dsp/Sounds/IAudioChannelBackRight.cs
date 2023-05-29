namespace Flux.Dsp
{
  /// <summary>Audio channel back right.</summary>
  public interface IAudioChannelBackRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf BackRight { get; }
  }
}
