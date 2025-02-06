namespace Flux.Dsp
{
  /// <summary>Audio channel front left.</summary>
  public interface IAudioChannelFrontLeft<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    static readonly System.Drawing.Color Color = System.Drawing.Color.White;

    TSelf FrontLeft { get; }
  }
}
