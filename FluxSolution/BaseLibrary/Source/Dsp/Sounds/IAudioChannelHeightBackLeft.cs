namespace Flux.Dsp
{
  /// <summary>Audio channel left height 2.</summary>
  public interface IAudioChannelHeightBackLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.Pink;

    TSelf HeightBackLeft { get; }
  }
}
