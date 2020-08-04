
namespace Flux.Dsp.AudioProcessor
{
  public class CustomMono : IAudioProcessorMono
  {
    private readonly System.Func<double, double> m_function;

    public CustomMono(System.Func<double, double> function) => m_function = function;

    public ISampleMono ProcessAudio(ISampleMono sample) => new MonoSample(m_function(sample.FrontCenter));
  }

  public class CustomStereo : IAudioProcessorStereo
  {
    private readonly System.Func<double, double, (double left, double right)> m_function;

    public CustomStereo(System.Func<double, double, (double left, double right)> function) => m_function = function;

    public ISampleStereo ProcessAudio(ISampleStereo sample) => m_function(sample.FrontLeft, sample.FrontRight) is var stereo ? new StereoSample(stereo.left, stereo.right) : throw new System.Exception();
  }
}
