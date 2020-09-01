
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

  public class CustomStereo
    : IWaveProcessorStereo
  {
    private readonly System.Func<double, double, (double left, double right)> m_function;

    public CustomStereo(System.Func<double, double, (double left, double right)> function)
      => m_function = function;

    public StereoSample ProcessAudio(StereoSample sample)
      => m_function(sample.FrontLeft, sample.FrontRight) is var stereo ? new StereoSample(stereo.left, stereo.right) : throw new System.Exception();
  }
}
