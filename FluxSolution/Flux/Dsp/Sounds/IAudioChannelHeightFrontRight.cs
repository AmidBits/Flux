namespace Flux.Dsp
{
  /// <summary>Audio channel right height 1.</summary>
  public interface IAudioChannelHeightFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Orange;

    TSelf HeightFrontRight { get; }
  }
}
