namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class CosineWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    public double GenerateMonoWaveUi(double phase)
      => SampleUi(phase);
    public double GenerateMonoWavePi2(double phase)
      => SamplePi2(phase);

    /// <summary>Generates a cosine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
      => System.Math.Cos(phaseUi * GenericMath.PiX2);
    /// <summary>Generates a cosine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => System.Math.Cos(phasePi2);
  }
}