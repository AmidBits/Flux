namespace Flux.Dsp.WaveGenerators
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class SineWave
    : /*IMonoWaveGeneratable,*/ IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    /// <summary>Generates a sine wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
      => double.Sin(phaseUi * double.Tau);

    /// <summary>Generates a sine wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => double.Sin(phasePi2);

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (Waves.WaveMono<double>)SampleUi(phaseUi);

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (Waves.WaveMono<double>)SamplePi2(phasePi2);

    //public IWaveMono<double> GenerateMonoWave(double phase, double cycle) => (WaveMono<double>)double.Sin(phase);
  }
}
