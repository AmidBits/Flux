namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public sealed class TangentWave
    : IMonoWaveMuGeneratable, IMonoWavePi2Generatable
  {
    public double GenerateMonoWaveMu(double phaseMu)
      => SampleMu(phaseMu);
    public double GenerateMonoWavePi2(double phasePi2)
      => SamplePi2(phasePi2);

    /// <summary>Generates a cosine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu)
      => System.Math.Tan(phaseMu * System.Math.PI);
    /// <summary>Generates a cosine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => System.Math.Tan(phasePi2 / 2);
  }
}
