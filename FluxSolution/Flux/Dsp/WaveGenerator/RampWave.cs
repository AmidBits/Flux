namespace Flux.Dsp.WaveGenerator
{
  /// <summary>This is simply an inverted saw wave.</summary>
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public record class RampWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    /// <summary>Generates a ramp (inverted saw) wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
      => Tools.AbsolutePhaseUi(phaseUi) * 2 - 1;

    /// <summary>Generates a ramp (inverted saw) wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => Tools.AbsolutePhasePi2(phasePi2) / System.Math.PI - 1;

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (Waves.WaveMono<double>)SampleUi(phaseUi);

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (Waves.WaveMono<double>)SamplePi2(phasePi2);
  }
}
