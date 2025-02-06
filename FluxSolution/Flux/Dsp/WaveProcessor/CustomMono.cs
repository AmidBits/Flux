namespace Flux.Dsp.WaveProcessor
{
  public record class CustomMono
    : IMonoWaveProcessable
  {
    private readonly System.Func<double, double> m_function;

    public CustomMono(System.Func<double, double> function) => m_function = function;

    public double ProcessMonoWave(double mono) => m_function(mono);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)m_function(mono.Wave);
  }
}
