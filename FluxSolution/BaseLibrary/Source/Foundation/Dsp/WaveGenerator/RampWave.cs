namespace Flux.Dsp.WaveGenerator
{
  /// <summary>This is simply an inverted saw wave.</summary>
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public class RampWave
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => -1 + phase * 2;

    /// <summary>Generates a ramp (inverted saw) wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu)
      => -1 + Tools.AbsolutePhaseMu(phaseMu) * 2;
    /// <summary>Generates a ramp (inverted saw) wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2 * <see cref="System.Math.PI"/>.</summary>
    public static double SamplePi2(double phasePi2)
      => -1 + Tools.AbsolutePhasePi2(phasePi2) / System.Math.PI;
  }
}
