namespace Flux.Dsp.AudioChannels
{
  /// <summary>Audio channel side left.</summary>
  public interface IAudioChannelSideLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Blue;

    TSelf SideLeft { get; }
  }
}
