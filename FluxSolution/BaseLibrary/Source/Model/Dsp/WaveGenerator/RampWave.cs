namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sawtooth_wave"/>
  public class RampWave
    : IWaveGenerator
  {
    public ISampleMono GenerateWave(double phase)
      => new MonoSample(-1 + phase * 2);

    // public static double Sample(double phase) => -1.0 + phase * 2.0;
  }
}
