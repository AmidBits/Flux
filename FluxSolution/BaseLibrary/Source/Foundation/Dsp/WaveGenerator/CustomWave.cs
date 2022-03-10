namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Sine_wave"/>
  public sealed class CustomWave
    : IMonoWaveMuGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Func<double, double> m_functionMu;
    private readonly System.Func<double, double> m_functionPi2;

    public CustomWave(System.Func<double, double> functionMu, System.Func<double, double> functionPi2)
    {
      m_functionMu = functionMu;
      m_functionPi2 = functionPi2;
    }

    public double GenerateMonoWaveMu(double phaseMu)
      => m_functionMu(phaseMu);
    public double GenerateMonoWavePi2(double phasePi2)
      => m_functionPi2(phasePi2);
  }
}
