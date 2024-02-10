namespace Flux
{
  public static partial class Em
  {
    public static Dsp.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.IWaveQuad<TSelf> wave)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveMono<TSelf>((wave.WaveBackLeft + wave.WaveBackRight + wave.WaveFrontLeft + wave.WaveFrontRight) / TSelf.CreateChecked(4));

    public static Dsp.IWaveStereo<TSelf> ToStereoWave<TSelf>(this Dsp.IWaveQuad<TSelf> wave)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveStereo<TSelf>((wave.WaveBackLeft + wave.WaveFrontLeft) / TSelf.CreateChecked(2), (wave.WaveBackRight + wave.WaveFrontRight) / TSelf.CreateChecked(2));
  }

  namespace Dsp
  {
    /// <summary>Quad (back left, back right, front left and front right) wave, range [-1, +1].</summary>
    public interface IWaveQuad<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      TSelf WaveBackLeft { get; }
      TSelf WaveBackRight { get; }
      TSelf WaveFrontLeft { get; }
      TSelf WaveFrontRight { get; }
    }
  }
}
