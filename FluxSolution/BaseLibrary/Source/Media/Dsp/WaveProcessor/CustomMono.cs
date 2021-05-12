namespace Flux.Dsp.AudioProcessor
{
  public class CustomMono
    : IWaveProcessorMono
  {
    private readonly System.Func<double, double> m_function;

    public CustomMono(System.Func<double, double> function)
      => m_function = function;

    public double ProcessAudio(double sample)
      => (m_function(sample));
  }
}
