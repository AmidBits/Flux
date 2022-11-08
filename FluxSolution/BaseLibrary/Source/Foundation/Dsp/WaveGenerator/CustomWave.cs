namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public sealed class CustomWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Func<double, double> m_functionUi;
    private readonly System.Func<double, double> m_functionPi2;

    public CustomWave(System.Func<double, double> functionUi, System.Func<double, double> functionPi2)
    {
      m_functionUi = functionUi;
      m_functionPi2 = functionPi2;
    }

    public double GenerateMonoWaveUi(double phaseUi)
      => m_functionUi(phaseUi);
    public double GenerateMonoWavePi2(double phasePi2)
      => m_functionPi2(phasePi2);
  }
}
