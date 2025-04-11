namespace Flux.Dsp.AudioChannels
{
  /// <summary>Audio channel (front) center.</summary>
  public interface IAudioChannelCenter<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Green;

    TSelf Center { get; }
  }
}
