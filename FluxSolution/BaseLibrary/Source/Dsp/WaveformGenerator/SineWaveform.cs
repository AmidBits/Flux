namespace Flux.Dsp.WaveformGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class PeriodicWaveform
  {
    public static double Sine(double x, double p = System.Math.Tau)
      => System.Math.Sin(x / p * System.Math.Tau);

    public static double Sawtooth(double x, double p = System.Math.Tau)
      => 2 * (x / p - System.Math.Floor(0.5 + x / p));

    public static double Triangle(double x, double p = System.Math.Tau)
    {
      var f = System.Math.Floor(2 * x / p + 0.5);

      return 4 / p * (x - p / 2 * f) * System.Math.Pow(-1, f);
    }

    public static double Square(double x, double p = System.Math.Tau)
      => x % p < p / 2 ? 1 : -1;
  }
}
