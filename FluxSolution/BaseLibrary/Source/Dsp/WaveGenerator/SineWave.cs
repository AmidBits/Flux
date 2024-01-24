namespace Flux.Dsp.WaveGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class SineWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    public IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (WaveMono<double>)SampleUi(phaseUi);
    public IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (WaveMono<double>)SamplePi2(phasePi2);

    /// <summary>Generates a sine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
      => System.Math.Sin(phaseUi * System.Math.Tau);
    /// <summary>Generates a sine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => System.Math.Sin(phasePi2);
  }
}
