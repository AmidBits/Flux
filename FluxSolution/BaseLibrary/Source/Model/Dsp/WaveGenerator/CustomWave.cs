namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public class CustomWave
    : IWaveGenerator
  {
    private readonly System.Func<double, double> m_function;

    public CustomWave(System.Func<double, double> function)
      => m_function = function;

    public MonoSample GenerateWave(double phase)
      => new MonoSample(m_function(phase));
  }
}
