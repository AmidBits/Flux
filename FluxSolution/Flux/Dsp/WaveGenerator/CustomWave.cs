namespace Flux.Dsp.WaveGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public record class CustomWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Func<double, double> m_functionUi;
    private readonly System.Func<double, double> m_functionPi2;

    public CustomWave(System.Func<double, double> functionUi, System.Func<double, double> functionPi2)
    {
      m_functionUi = functionUi;
      m_functionPi2 = functionPi2;
    }

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (Waves.WaveMono<double>)m_functionUi(phaseUi);

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (Waves.WaveMono<double>)m_functionPi2(phasePi2);
  }
}
