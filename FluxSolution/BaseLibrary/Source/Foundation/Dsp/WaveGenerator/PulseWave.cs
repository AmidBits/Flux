namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
  public class PulseWave
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

    // public static double Sample(double phase, double dutyCycle) => phase < dutyCycle ? 1.0 : -1.0;
  }
}
