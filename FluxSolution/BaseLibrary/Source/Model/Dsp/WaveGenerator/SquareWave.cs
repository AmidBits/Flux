namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
  public class SquareWave
    : IWaveGenerator
  {
    public ISampleMono GenerateWave(double phase)
      => new MonoSample(phase < 0.5 ? 1 : -1);

    // public static double Sample(double phase) => phase < 0.5 ? 1.0 : -1.0;
  }
}
