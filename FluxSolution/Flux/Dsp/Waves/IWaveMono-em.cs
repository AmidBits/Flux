namespace Flux
{
  public static partial class DspExtensions
  {
    public static Dsp.Waves.IWaveStereo<TSelf> ToStereoWave<TSelf>(this Dsp.Waves.IWaveMono<TSelf> mono)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.Waves.WaveStereo<TSelf>(mono.Wave, mono.Wave);
  }
}
