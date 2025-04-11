namespace Flux.Dsp.WaveGenerators
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public record class SawWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    public static double SampleUi(double phaseUi)
     => 1 - Tools.AbsolutePhaseUi(phaseUi) * 2;

    /// <summary>Generates a saw wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => 1 - Tools.AbsolutePhasePi2(phasePi2) / double.Pi;

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (Waves.WaveMono<double>)SampleUi(phaseUi);

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (Waves.WaveMono<double>)SamplePi2(phasePi2);
  }
}
