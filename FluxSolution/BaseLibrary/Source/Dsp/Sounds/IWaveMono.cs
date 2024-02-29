namespace Flux
{
  public static partial class Em
  {
#if NET7_0_OR_GREATER
    public static Dsp.IWaveStereo<TSelf> ToStereoWave<TSelf>(this Dsp.IWaveMono<TSelf> mono)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveStereo<TSelf>(mono.Wave, mono.Wave);
#else
    public static Dsp.IWaveStereo<double> ToStereoWave(this Dsp.IWaveMono<double> mono)
      => new Dsp.WaveStereo<double>(mono.Wave, mono.Wave);
#endif
  }

  namespace Dsp
  {
    /// <summary>Mono wave, range [-1, +1].</summary>
    public interface IWaveMono<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      TSelf Wave { get; }
    }
  }
}
