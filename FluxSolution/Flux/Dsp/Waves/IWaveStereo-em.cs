namespace Flux
{
  public static partial class Fx
  {
    public static Dsp.Waves.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.Waves.IWaveStereo<TSelf> stereo)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.Waves.WaveMono<TSelf>((stereo.SampleLeft + stereo.SampleRight) / TSelf.CreateChecked(2));
  }
}
