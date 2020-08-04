namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public class SineWave
    : IWaveGenerator
  {
    public ISampleMono GenerateWave(double phase)
      => new MonoSample(System.Math.Sin(phase * Math.PiX2));

    // public static double Sample(double phase) => System.Math.Sin(phase * Math.PiX2);
  }
}
