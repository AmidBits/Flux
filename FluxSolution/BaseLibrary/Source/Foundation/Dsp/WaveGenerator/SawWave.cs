namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public class SawWave
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => 1 - phase * 2;

    public static double SampleMu(double phaseMu)
     => 1 - Tools.AbsolutePhaseMu(phaseMu) * 2;
    /// <summary>Generates a saw wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => 1 - Tools.AbsolutePhasePi2(phasePi2) / System.Math.PI;

  }
}
