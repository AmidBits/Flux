namespace Flux
{
  public static partial class Em
  {
    public static Dsp.Waves.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.Waves.IWaveQuad<TSelf> wave)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.Waves.WaveMono<TSelf>((wave.WaveBackLeft + wave.WaveBackRight + wave.WaveFrontLeft + wave.WaveFrontRight) / TSelf.CreateChecked(4));

    public static Dsp.Waves.IWaveStereo<TSelf> ToStereoWave<TSelf>(this Dsp.Waves.IWaveQuad<TSelf> wave)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.Waves.WaveStereo<TSelf>((wave.WaveBackLeft + wave.WaveFrontLeft) / TSelf.CreateChecked(2), (wave.WaveBackRight + wave.WaveFrontRight) / TSelf.CreateChecked(2));
  }
}
