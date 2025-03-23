namespace Flux.Dsp.WaveformGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class PeriodicWaveform
  {
    public static double Saw(double phase, double wavePeriod, double superScalar = 1)
      => phase % (wavePeriod / superScalar);

    public static double WaveEquation(double x, double waveLength)
      => (double.Tau / waveLength * x);

    public static double WaveEquation(double x, double waveLength, double time, double period, double phaseShift)
      => (double.Tau / waveLength * x) - (double.Tau / period * time) + phaseShift;

    public static double SawtoothWave(double time, double waveLength, double amplitude, double phase)
      => (2 * amplitude / double.Pi) * double.Atan(double.Tan((double.Tau * time - phase) / (2 * waveLength)));

    public static double SineWave(double time, double waveLength, double amplitude, double phase)
      => amplitude * double.Sin((double.Tau * time - phase) / waveLength);

    public static double SquareWave(double time, double waveLength, double amplitude, double phase)
    => double.CopySign(amplitude, SineWave(time, waveLength, amplitude, phase));

    public static double TriangleWave(double time, double waveLength, double amplitude, double phase)
      => (2 * amplitude / double.Pi) * double.Asin(double.Sin((double.Tau * time - phase) / waveLength));

    public static double SawtoothWave(double phase, double period = double.Tau)
      => 2 * (phase / period - double.Floor(0.5 + phase / period));

    public static double SineWave(double phase, double period = double.Tau)
      => double.Sin(phase / period * double.Tau);

    public static double SquareWave(double phase, double period = double.Tau)
      => phase % period < period / 2 ? 1 : -1;

    public static double TriangleWave(double phase, double period = double.Tau)
    {
      var f = double.Floor(2 * phase / period + 0.5);

      return 4 / period * (phase - period / 2 * f) * double.Pow(-1, f);
    }
  }
}
