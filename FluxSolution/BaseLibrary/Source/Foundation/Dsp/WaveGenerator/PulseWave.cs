namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
#if NET5_0
  public sealed class PulseWave
#else
  public record struct PulseWave
#endif
    : IWaveGenerator
  {
    private double m_dutyCycle;
    /// <summary>The duty cycle (pulse width) in the range [0, 2PI].</summary>
    public double DutyCycle { get => m_dutyCycle; set => m_dutyCycle = Maths.Wrap(value, 0.0, 1.0); }

    public PulseWave(double dutyCycle)
      => m_dutyCycle = Maths.Wrap(dutyCycle, 0.0, 1.0);
    public PulseWave()
      : this(0.5)
    { }

    public double GenerateWave(double phase)
      => phase < 0.5 ? 1 : -1;

    /// <summary>Generates a pulse wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleMu(double phaseMu, double pulseWidthMu)
      => Tools.AbsolutePhaseMu(phaseMu) < Tools.AbsolutePhaseMu(pulseWidthMu)
      ? 1
      : -1;
    /// <summary>Generates a pulse wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2, double pulseWidthPi2)
      => Tools.AbsolutePhasePiX2(phasePi2) < Tools.AbsolutePhasePiX2(pulseWidthPi2)
      ? 1
      : -1;
  }
}
