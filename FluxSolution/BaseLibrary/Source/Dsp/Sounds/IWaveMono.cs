namespace Flux
{
  public static partial class ExtensionMethodsDsp
  {
    public static Dsp.IWaveStereo<TSelf> ToStereoWave<TSelf>(this Dsp.IWaveMono<TSelf> mono)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveStereo<TSelf>(mono.Wave, mono.Wave);
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
