namespace Flux
{
  public static partial class ExtensionMethodsDsp
  {
    public static Dsp.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.IWaveStereo<TSelf> stereo)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveMono<TSelf>((stereo.LeftWave + stereo.RightWave).Divide(2));
  }

  namespace Dsp
  {
    /// <summary>Stereo (left and right) wave, range [-1, +1].</summary>
    public interface IWaveStereo<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      TSelf LeftWave { get; }
      TSelf RightWave { get; }
    }
  }
}
