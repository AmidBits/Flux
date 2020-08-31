namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Triangle_wave"/>
  public class TriangleWave
    : IWaveGenerator
  {
    public double GenerateWave(double phase)
      => (phase < 0.5 ? -1 + phase * 4 : 3 - phase * 4);

    // public static double Sample(double phase) => phase < 0.5 ? -1.0 + phase * 4.0 : 3.0 - phase * 4.0;
  }
}
