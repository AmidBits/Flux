namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
  public class PulseWave
    : IWaveGenerator
  {
    private double? _dutyCycle;
    /// <summary>The duty cycle (pulse width) in the range [0, 2PI].</summary>
    public double? DutyCycle { get => _dutyCycle; set => _dutyCycle = value.HasValue ? Maths.Wrap(value.Value, 0.0, 1.0) : default; }

    public PulseWave(double dutyCycle)
      => _dutyCycle = Maths.Wrap(dutyCycle, 0.0, 1.0);
    public PulseWave() : this(System.Math.PI) { }

    public double GenerateWave(double phase)
      => (phase < 0.5 ? 1 : -1);

    // public static double Sample(double phase, double dutyCycle) => phase < dutyCycle ? 1.0 : -1.0;
  }
}
