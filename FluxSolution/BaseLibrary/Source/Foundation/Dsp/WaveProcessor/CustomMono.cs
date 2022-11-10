namespace Flux.Dsp.AudioProcessor
{
  public record class CustomMono
    : IMonoWaveProcessable
  {
    private readonly System.Func<double, double> m_function;

    public CustomMono(System.Func<double, double> function)
      => m_function = function;

    public double ProcessMonoWave(double sample)
      => (m_function(sample));
  }
}
