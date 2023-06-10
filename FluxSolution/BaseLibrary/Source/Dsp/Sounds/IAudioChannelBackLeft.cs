namespace Flux.Dsp
{
  /// <summary>Audio channel back left.</summary>
  public interface IAudioChannelBackLeft<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    TSelf BackLeft { get; }
  }
}
