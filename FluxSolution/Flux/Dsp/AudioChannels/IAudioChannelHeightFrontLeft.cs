namespace Flux.Dsp.AudioChannels
{
  /// <summary>Audio channel left height 1.</summary>
  public interface IAudioChannelHeightFrontLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Yellow;

    TSelf HeightFrontLeft { get; }
  }
}
