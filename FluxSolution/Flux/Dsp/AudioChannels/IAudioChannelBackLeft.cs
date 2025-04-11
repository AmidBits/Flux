namespace Flux.Dsp.AudioChannels
{
  /// <summary>Audio channel back left.</summary>
  public interface IAudioChannelBackLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Brown;

    TSelf BackLeft { get; }
  }
}
