namespace Flux.Dsp.WaveGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public record class SawWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    public IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (WaveMono<double>)SampleUi(phaseUi);
    public IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (WaveMono<double>)SamplePi2(phasePi2);

    public static double SampleUi(double phaseUi)
     => 1 - Tools.AbsolutePhaseUi(phaseUi) * 2;
    /// <summary>Generates a saw wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => 1 - Tools.AbsolutePhasePi2(phasePi2) / System.Math.PI;
  }
}
