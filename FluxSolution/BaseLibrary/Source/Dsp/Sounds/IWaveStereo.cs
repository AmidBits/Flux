namespace Flux
{
  public static partial class ExtensionMethodsDsp
  {
#if NET7_0_OR_GREATER
    public static Dsp.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.IWaveStereo<TSelf> stereo)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveMono<TSelf>((stereo.LeftWave + stereo.RightWave).Divide(2));
#else
    public static Dsp.IWaveMono<double> ToMonoWave(this Dsp.IWaveStereo<double> stereo)
      => new Dsp.WaveMono<double>((stereo.LeftWave + stereo.RightWave) / 2);
#endif
  }

  namespace Dsp
  {
    /// <summary>Stereo (left and right) wave, range [-1, +1].</summary>
    public interface IWaveStereo<TSelf>
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
#endif
    {
      TSelf LeftWave { get; }
      TSelf RightWave { get; }
    }
  }
}
