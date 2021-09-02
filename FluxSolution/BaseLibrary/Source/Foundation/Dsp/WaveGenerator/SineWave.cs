namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public class SineWave
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => System.Math.Sin(phase * Maths.PiX2);

    /// <summary>Generates a sine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu)
      => System.Math.Sin(phaseMu * Maths.PiX2);
    /// <summary>Generates a sine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => System.Math.Sin(phasePi2);
  }
}
