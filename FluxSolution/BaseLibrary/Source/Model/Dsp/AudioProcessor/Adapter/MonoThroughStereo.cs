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

    public MonoSample ProcessAudio(MonoSample sample)
      => Stereo.ProcessAudio(sample.ToStereo()).ToMono();
  }
}
