namespace Flux.Dsp.WaveGenerator
{
  /// <summary>This is simply an inverted saw wave.</summary>
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public class RampWave
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => phase * 2 - 1;

    /// <summary>Generates a ramp (inverted saw) wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu)
      => Tools.AbsolutePhaseMu(phaseMu) * 2 - 1;
    /// <summary>Generates a ramp (inverted saw) wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => Tools.AbsolutePhasePi2(phasePi2) / System.Math.PI - 1;
  }
}
