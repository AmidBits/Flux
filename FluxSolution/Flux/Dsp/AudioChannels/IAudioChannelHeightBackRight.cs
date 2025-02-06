namespace Flux.Dsp
{
  /// <summary>Audio channel right height 2.</summary>
  public interface IAudioChannelHeightBackRight<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Magenta;

    TSelf HeightBackRight { get; }
  }
}
