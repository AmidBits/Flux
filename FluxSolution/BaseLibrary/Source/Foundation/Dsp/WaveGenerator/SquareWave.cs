namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
#if NET5_0
  public sealed class SquareWave
#else
  public record struct SquareWave
#endif
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => phase < 0.5 ? 1 : -1;

    /// <summary>Generates a square wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu)
      => Tools.AbsolutePhaseMu(phaseMu) < 0.5
      ? 1
      : -1;
    /// <summary>Generates a square wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SquarePi2(double phasePi2)
      => Tools.AbsolutePhasePiX2(phasePi2) < System.Math.PI
      ? 1
      : -1;
  }
}
