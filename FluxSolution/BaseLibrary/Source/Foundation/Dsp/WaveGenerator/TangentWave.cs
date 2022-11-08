namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public sealed class TangentWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    public double GenerateMonoWaveUi(double phaseUi)
      => SampleUi(phaseUi);
    public double GenerateMonoWavePi2(double phasePi2)
      => SamplePi2(phasePi2);

    /// <summary>Generates a cosine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
      => System.Math.Tan(phaseUi * System.Math.PI);
    /// <summary>Generates a cosine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => System.Math.Tan(phasePi2 / 2);
  }
}
