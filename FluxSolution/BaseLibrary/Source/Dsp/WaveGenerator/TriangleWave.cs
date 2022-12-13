namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Triangle_wave"/>
  public record class TriangleWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    //public double GenerateMonoWave(double phase)
    //  => phase < 0.5 ? -1 + phase * 4 : 3 - phase * 4;
    public double GenerateMonoWaveUi(double phaseUi)
      => SampleUi(phaseUi);
    public double GenerateMonoWavePi2(double phasePi2)
      => SamplePi2(phasePi2);

    /// <summary>Generates a triangle wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi)
     => Tools.AbsolutePhaseUi(phaseUi) is var phaseAbs && phaseAbs < 0.5
     ? phaseAbs * 4 - 1
     : 3 - phaseAbs * 4;
    /// <summary>Generates a triangle wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2)
      => Tools.AbsolutePhasePi2(phasePi2) is var phaseAbs && phaseAbs < System.Math.PI
      ? phaseAbs / GenericMath.PiOver2 - 1
      : 3 - phaseAbs / GenericMath.PiOver2;
  }
}
