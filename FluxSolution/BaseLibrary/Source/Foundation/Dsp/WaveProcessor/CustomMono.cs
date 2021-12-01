namespace Flux.Dsp.AudioProcessor
{
#if NET5_0
  public sealed class CustomMono
#else
  public record struct CustomMono
#endif
    : IWaveProcessorMono
  {
    private readonly System.Func<double, double> m_function;

    public CustomMono(System.Func<double, double> function)
      => m_function = function;

    public double ProcessAudio(double sample)
      => (m_function(sample));
  }
}
