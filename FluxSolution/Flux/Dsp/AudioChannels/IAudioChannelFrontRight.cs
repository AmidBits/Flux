namespace Flux.Dsp.AudioChannels
{
  /// <summary>Audio channel front right.</summary>
  public interface IAudioChannelFrontRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Red;

    TSelf FrontRight { get; }
  }
}
