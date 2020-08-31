namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoThroughStereo
    : IAudioProcessorMono
  {
    public IAudioProcessorStereo Stereo { get; }

    public MonoThroughStereo(IAudioProcessorStereo stereo)
    {
      Stereo = stereo;
    }

    public double ProcessAudio(double sample)
      => Stereo.ProcessAudio(new StereoSample(sample)).ToMono();
  }
}
