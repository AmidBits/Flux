namespace Flux.Dsp.WaveformGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class PeriodicWaveform
  {
    public static double WaveEquation(double x, double waveLength)
      => (System.Math.Tau / waveLength * x);

    public static double WaveEquation(double x, double waveLength, double time, double period, double phaseShift)
      => (System.Math.Tau / waveLength * x) - (System.Math.Tau / period * time) + phaseShift;

    public static double SawtoothWave(double time, double waveLength, double amplitude, double phase)
      => (2 * amplitude / System.Math.PI) * System.Math.Atan(System.Math.Tan((System.Math.Tau * time - phase) / (2 * waveLength)));

    public static double SineWave(double time, double waveLength, double amplitude, double phase)
      => amplitude * System.Math.Sin((System.Math.Tau * time - phase) / waveLength);

    public static double SquareWave(double time, double waveLength, double amplitude, double phase)
    => double.CopySign(amplitude, SineWave(time, waveLength, amplitude, phase));

    public static double TriangleWave(double time, double waveLength, double amplitude, double phase)
      => (2 * amplitude / System.Math.PI) * System.Math.Asin(System.Math.Sin((System.Math.Tau * time - phase) / waveLength));

    public static double SawtoothWave(double phase, double period = System.Math.Tau)
      => 2 * (phase / period - System.Math.Floor(0.5 + phase / period));

    public static double SineWave(double phase, double period = System.Math.Tau)
      => System.Math.Sin(phase / period * System.Math.Tau);

    public static double SquareWave(double phase, double period = System.Math.Tau)
      => phase % period < period / 2 ? 1 : -1;

    public static double TriangleWave(double phase, double period = System.Math.Tau)
    {
      var f = System.Math.Floor(2 * phase / period + 0.5);

      return 4 / period * (phase - period / 2 * f) * System.Math.Pow(-1, f);
    }
  }
}
