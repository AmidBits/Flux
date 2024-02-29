namespace Flux.Dsp
{
  /// <summary>Audio channel back right.</summary>
  public interface IAudioChannelBackRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Khaki;

    TSelf BackRight { get; }
  }
}
