namespace Flux
{
  public static partial class Fx
  {
    public static Dsp.IWaveMono<TSelf> ToMonoWave<TSelf>(this Dsp.IWaveStereo<TSelf> stereo)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new Dsp.WaveMono<TSelf>((stereo.SampleLeft + stereo.SampleRight) / TSelf.CreateChecked(2));
  }

  namespace Dsp
  {
    /// <summary>Stereo (left and right) wave, range [-1, +1].</summary>
    public interface IWaveStereo<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      TSelf SampleLeft { get; }
      TSelf SampleRight { get; }
    }
  }
}
