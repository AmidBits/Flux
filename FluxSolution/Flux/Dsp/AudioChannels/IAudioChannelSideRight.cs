namespace Flux.Dsp
{
  /// <summary>Audio channel side right.</summary>
  public interface IAudioChannelSideRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Gray;

    TSelf SideRight { get; }
  }
}
